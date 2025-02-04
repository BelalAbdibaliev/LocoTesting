using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Application.Interfaces.Services;
using LocoTesting.Domain.Models;

namespace LocoTesting.Application.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;

    public TestService(ITestRepository testRepository)
    {
        _testRepository = testRepository;
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

    public async Task<List<QuestionDto>?> GetAllQuestionsAsync(int testId)
    {
        if(!await _testRepository.CheckTestExistsAsync(testId))
            throw new KeyNotFoundException("Test does not exist");
        
        var questions = await _testRepository.GetQuestionsAsync(testId);
        
        var questionsDto = questions.Select(a => new QuestionDto
        {
            Id = a.Id,
            Text = a.Text,
            Content = a.Content,
            TestId = a.TestId,
            Answers = a.Answers.Select(b => new AnswerDto
            {
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

    public async Task<QuestionDto> AddQuestionAsync(CreateQuestionDto dto)
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
            Answers = dto.AnswersDto.Select(a => new Answer
            {
                Text = a.Text,
                IsCorrect = a.IsCorrect,
            }).ToList()
        };
        
        var result = await _testRepository.CreateQuestionAsync(question);
        
        return new QuestionDto
        {
            Id = result.Id,
            Text = result.Text,
            Content = result.Content,
            TestId = result.TestId,
            Answers = result.Answers.Select(a => new AnswerDto
            {
                Text = a.Text,
                IsCorrect = a.IsCorrect
            }).ToList()
        };
    }

    public async Task<AnswerDto> AddAnswerAsync(CreateAnswerDto dto)
    {
        if(dto == null)
            throw new ArgumentNullException("DTO cannot be null");
        
        if(!await _testRepository.CheckQuestionExistsAsync(dto.QuestionId))
            throw new KeyNotFoundException("Question does not exist");
        
        if(dto.IsCorrect && await _testRepository.CheckIsTrueAnswerExists(dto.QuestionId))
            throw new ArgumentException("True answer already exists");
        
        var answer = new Answer
        {
            Text = dto.Text,
            IsCorrect = dto.IsCorrect,
            QuestionId = dto.QuestionId,
        };
        
        var result = await _testRepository.CreateAnswerAsync(answer);
        if(result == null)
            throw new NullReferenceException("Bad shit happened");

        return new AnswerDto
        {
            Text = result.Text,
            IsCorrect = result.IsCorrect
        };
    }
}