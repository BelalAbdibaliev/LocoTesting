using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Question;
using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces;
using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Application.Interfaces.Services;
using LocoTesting.Domain.Models;

namespace LocoTesting.Application.Services;

public class TestService : ITestService
{
    private readonly IUnitOfWork _unitOfWork;

    public TestService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<TestDto>> GetAllTestsAsync()
    {
        var tests = await _unitOfWork.Tests.GetAllTestsAsync();

        var testDtos = tests.Select(test => new TestDto
        {
            Id = test.Id,
            Title = test.Title,
            Description = test.Description
        }).ToList();
        
        return testDtos;
    }

    public async Task<List<QuestionResponseDto>?> GetAllQuestionsAsync(int testId)
    {
        if(!await _unitOfWork.Tests.IsTestExistsAsync(testId))
            throw new KeyNotFoundException("Test does not exist");
        
        var questions = await _unitOfWork.Questions.GetQuestionsAsync(testId);
        
        var questionsDto = questions.Select(a => new QuestionResponseDto
        {
            Id = a.Id,
            Text = a.Text,
            Content = a.Content,
            TestId = a.TestId,
            Options = a.Options.Select(b => new OptionResponseDto()
            {
                Id = b.Id,
                IsCorrect = b.IsCorrect,
                Text = b.Text,
            }).ToList(),
        }).ToList();
        
        return questionsDto;
    }

    public async Task<TestDto> AddTestAsync(CreateTestDto dto)
    {
        if(dto == null)
            throw new ArgumentNullException("DTO cannot be null");
        
        var test = new Test
        {
            Title = dto.Title,
            Description = dto.Description,
        };
        
        var result = await _unitOfWork.Tests.CreateTestAsync(test);
        return new TestDto
        {
            Id = result.Id,
            Title = test.Title,
            Description = test.Description,
        };
    }

    public async Task<QuestionResponseDto> AddQuestionAsync(CreateQuestionDto dto)
    {
        if(dto == null)
            throw new ArgumentNullException("DTO cannot be null");
        if(!await _unitOfWork.Tests.IsTestExistsAsync(dto.TestId))
            throw new KeyNotFoundException("Test does not exist");
        
        var question = new Question
        {
            Text = dto.Question,
            Content = dto.Content,
            TestId = dto.TestId,
        };
        
        var result = await _unitOfWork.Questions.CreateQuestionAsync(question);
        
        return new QuestionResponseDto
        {
            Id = result.Id,
            Text = result.Text,
            Content = result.Content,
            TestId = result.TestId,
            Options = result.Options.Select(a => new OptionResponseDto()
            {
                Id = a.Id,
                Text = a.Text,
                IsCorrect = a.IsCorrect
            }).ToList()
        };
    }

    public async Task<OptionResponseDto> AddOptionAsync(CreateOptionDto dto)
    {
        if(dto == null)
            throw new ArgumentNullException("DTO cannot be null");
        
        if(!await _unitOfWork.Questions.IsQuestionExistsAsync(dto.QuestionId))
            throw new KeyNotFoundException("Question does not exist");
        
        if(dto.IsCorrect && await _unitOfWork.Options.IsTrueOptionExistsAsync(dto.QuestionId))
            throw new ArgumentException("True answer already exists");
        
        var answer = new Option
        {
            Text = dto.Text,
            IsCorrect = dto.IsCorrect,
            QuestionId = dto.QuestionId,
        };
        
        var result = await _unitOfWork.Options.CreateOptionAsync(answer);
        if(result == null)
            throw new NullReferenceException("Bad shit happened");

        return new OptionResponseDto
        {
            Id = result.Id,
            Text = result.Text,
            IsCorrect = result.IsCorrect
        };
    }
}