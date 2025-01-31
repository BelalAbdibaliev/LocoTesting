﻿using LocoTesting.Domain.Models;
using LocoTesting.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LocoTesting.Infrastructure.Repositories;

public interface ITestRepository
{
    Task<List<Test>?> GetAllTestsAsync();
    Task<Test?> CreateTestAsync(Test test);
    Task<Question?> CreateQuestionAsync(Question question);
    Task<Answer?> CreateAnswerAsync(Answer answer);
}

public class TestRepository: ITestRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TestRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Test>?> GetAllTestsAsync()
    {
        var tests = await _dbContext.Tests.ToListAsync();
        
        return tests;
    }

    public async Task<Test?> CreateTestAsync(Test test)
    {
        await _dbContext.Tests.AddAsync(test);
        await _dbContext.SaveChangesAsync();

        return await _dbContext.Tests.FirstOrDefaultAsync(t => t.Id == test.Id);
    }

    public async Task<Question?> CreateQuestionAsync(Question question)
    {
        await _dbContext.Questions.AddAsync(question);
        await _dbContext.SaveChangesAsync();
        
        return await _dbContext.Questions
            .Include(q => q.Answers)
            .FirstAsync(q => q.Id == question.Id);
    }

    public async Task<Answer?> CreateAnswerAsync(Answer answer)
    {
        await _dbContext.Answers.AddAsync(answer);
        await _dbContext.SaveChangesAsync();
        
        return await _dbContext.Answers.FirstAsync(a => a.Id == answer.Id);
    }
}