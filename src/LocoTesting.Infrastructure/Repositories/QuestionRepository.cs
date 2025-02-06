using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Domain.Models;
using LocoTesting.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LocoTesting.Infrastructure.Repositories;

public class QuestionRepository: IQuestionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public QuestionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Question>?> GetQuestionsAsync(int testId)
    {
        var questions = await _dbContext.Questions
            .Where(q => q.TestId == testId)
            .Include(a => a.Options)
            .ToListAsync();
        
        return questions;
    }
    
    public async Task<bool> IsQuestionExistsAsync(int id)
    {
        return await _dbContext.Questions.AnyAsync(t => t.Id == id);
    }
    
    public async Task<Question?> CreateQuestionAsync(Question question)
    {
        await _dbContext.Questions.AddAsync(question);
        await _dbContext.SaveChangesAsync();
        
        return await _dbContext.Questions
            .Include(q => q.Options)
            .FirstAsync(q => q.Id == question.Id);
    }
}