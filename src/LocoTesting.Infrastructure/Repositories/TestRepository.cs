using LocoTesting.Domain.Models;
using LocoTesting.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LocoTesting.Infrastructure.Repositories;

public interface ITestRepository
{
    Task<List<Test>?> GetAllTestsAsync();
    Task<Test?> GetTestByIdAsync(int id);
    Task<bool> CheckTestExistsAsync(int id);
    Task<bool> CheckQuestionExistsAsync(int id);
    Task<bool> CheckIsTrueAnswerExists(int questionId);
    Task<Test?> CreateTestAsync(Test test);
    Task<Question?> CreateQuestionAsync(Question question);
    Task<Answer?> CreateAnswerAsync(Answer answer);
    Task<List<Question>?> GetQuestionsAsync(int testId);
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

    public async Task<bool> CheckIsTrueAnswerExists(int questionId)
    {
        var answer = _dbContext.Answers
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
            .Include(q => q.Answers)
            .FirstAsync(q => q.Id == question.Id);
    }

    public async Task<Answer?> CreateAnswerAsync(Answer answer)
    {
        await _dbContext.Answers.AddAsync(answer);
        await _dbContext.SaveChangesAsync();
        
        return await _dbContext.Answers.FirstAsync(a => a.Id == answer.Id);
    }

    public async Task<List<Question>?> GetQuestionsAsync(int testId)
    {
        var questions = await _dbContext.Questions
            .Where(q => q.TestId == testId)
            .Include(a => a.Answers)
            .ToListAsync();
        
        return questions;
    }
}