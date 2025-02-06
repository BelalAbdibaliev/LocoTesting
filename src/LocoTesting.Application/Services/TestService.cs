using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Question;
using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Application.Interfaces.Services;
using LocoTesting.Domain.Models;

namespace LocoTesting.Application.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IOptionRepository _optionRepository;

    public TestService(ITestRepository testRepository, IQuestionRepository questionRepository, IOptionRepository optionRepository)
    {
        _testRepository = testRepository;
        _questionRepository = questionRepository;
        _optionRepository = optionRepository;
    }
    
    public async Task<List<TestDto>> GetAllTestsAsync()
    {
        var tests = await _testRepository.GetAllTestsAsync();

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
        if(!await _testRepository.CheckTestExistsAsync(testId))
            throw new KeyNotFoundException("Test does not exist");
        
        var questions = await _questionRepository.GetQuestionsAsync(testId);
        
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
        
        var result = await _testRepository.CreateTestAsync(test);
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
        if(!await _testRepository.CheckTestExistsAsync(dto.TestId))
            throw new KeyNotFoundException("Test does not exist");
        
        var question = new Question
        {
            Text = dto.Question,
            Content = dto.Content,
            TestId = dto.TestId,
        };
        
        var result = await _questionRepository.CreateQuestionAsync(question);
        
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
        
        if(!await _questionRepository.CheckQuestionExistsAsync(dto.QuestionId))
            throw new KeyNotFoundException("Question does not exist");
        
        if(dto.IsCorrect && await _optionRepository.CheckIsTrueOptionExists(dto.QuestionId))
            throw new ArgumentException("True answer already exists");
        
        var answer = new Option
        {
            Text = dto.Text,
            IsCorrect = dto.IsCorrect,
            QuestionId = dto.QuestionId,
        };
        
        var result = await _optionRepository.CreateOptionAsync(answer);
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