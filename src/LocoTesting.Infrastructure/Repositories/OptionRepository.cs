using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Domain.Entities;
using LocoTesting.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LocoTesting.Infrastructure.Repositories;

public class OptionRepository: IOptionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OptionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<AnswerOption?> CreateAsync(AnswerOption answerOption)
    {
        await _dbContext.AnswerOptions.AddAsync(answerOption);
        await _dbContext.SaveChangesAsync();
        
        return await _dbContext.AnswerOptions.FirstAsync(a => a.Id == answerOption.Id);
    }
    
    public async Task<bool> IsTrueAnswerOptionExistsAsync(int questionId)
    {
        var answer = _dbContext.AnswerOptions
            .Where(t => t.QuestionId == questionId)
            .AsQueryable();
        return await answer.Where(t => t.IsCorrect == true).AnyAsync();
    }
    
    public async Task<AnswerOption> GetCorrectAnswerOptionAsync(int questionId)
    {
        var correctOption = await _dbContext.AnswerOptions
            .FirstOrDefaultAsync(t => t.QuestionId == questionId && t.IsCorrect);
        return correctOption;
    }
}