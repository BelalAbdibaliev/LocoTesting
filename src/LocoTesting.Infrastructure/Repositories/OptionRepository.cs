using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Domain.Models;
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
    
    public async Task<Option?> CreateOptionAsync(Option option)
    {
        await _dbContext.Options.AddAsync(option);
        await _dbContext.SaveChangesAsync();
        
        return await _dbContext.Options.FirstAsync(a => a.Id == option.Id);
    }
    
    public async Task<bool> IsTrueOptionExistsAsync(int questionId)
    {
        var answer = _dbContext.Options
            .Where(t => t.QuestionId == questionId)
            .AsQueryable();
        return await answer.Where(t => t.IsCorrect == true).AnyAsync();
    }
    
    public async Task<Option> GetCorrectOptionAsync(int questionId)
    {
        var correctOption = await _dbContext.Options
            .FirstOrDefaultAsync(t => t.QuestionId == questionId && t.IsCorrect);
        return correctOption;
    }
}