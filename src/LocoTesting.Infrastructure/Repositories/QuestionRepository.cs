using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Domain.Entities;
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
    
    public async Task<List<Question>?> GetByTestIdAsync(int testId)
    {
        var questions = await _dbContext.Questions
            .Where(q => q.TestId == testId)
            .Include(a => a.AnswerOptions)
            .ToListAsync();
        
        return questions;
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _dbContext.Questions.AnyAsync(t => t.Id == id);
    }
}