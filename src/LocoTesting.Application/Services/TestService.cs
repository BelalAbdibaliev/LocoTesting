using System.Net.Mime;
using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces;
using LocoTesting.Domain.Models;
using LocoTesting.Infrastructure.Repositories;

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

    public async Task<TestDto> AddTestAsync(CreateTestDto dto)
    {
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