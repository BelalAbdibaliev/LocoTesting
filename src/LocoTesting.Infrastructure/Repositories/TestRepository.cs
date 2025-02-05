using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Domain.Models;
using LocoTesting.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LocoTesting.Infrastructure.Repositories;

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

    public async Task<Test?> GetTestByIdAsync(int id)
    {
        var test = await _dbContext.Tests.FirstOrDefaultAsync(t => t.Id == id);
        
        return test;
    }

    public async Task<bool> CheckTestExistsAsync(int id)
    {
        return await _dbContext.Tests.AnyAsync(t => t.Id == id);
    }

    public async Task<bool> CheckQuestionExistsAsync(int id)
    {
        return await _dbContext.Questions.AnyAsync(t => t.Id == id);
    }

    public async Task<bool> CheckIsTrueOptionExists(int questionId)
    {
        var answer = _dbContext.Options
            .Where(t => t.QuestionId == questionId)
            .AsQueryable();
        return await answer.Where(t => t.IsCorrect == true).AnyAsync();
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
            .Include(q => q.Options)
            .FirstAsync(q => q.Id == question.Id);
    }

    public async Task<Option?> CreateOptionAsync(Option option)
    {
        await _dbContext.Options.AddAsync(option);
        await _dbContext.SaveChangesAsync();
        
        return await _dbContext.Options.FirstAsync(a => a.Id == option.Id);
    }

    public async Task<List<Question>?> GetQuestionsAsync(int testId)
    {
        var questions = await _dbContext.Questions
            .Where(q => q.TestId == testId)
            .Include(a => a.Options)
            .ToListAsync();
        
        return questions;
    }
}