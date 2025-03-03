using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Domain.Entities;
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
    
    public async Task<List<Test>?> GetAllAsync()
    {
        var tests = await _dbContext.Tests.ToListAsync();
        
        return tests;
    }

    public async Task<Test?> GetByIdAsync(int id)
    {
        var test = await _dbContext.Tests.FirstOrDefaultAsync(t => t.Id == id);
        
        return test;
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _dbContext.Tests.AnyAsync(t => t.Id == id);
    }

    public async Task<Test?> CreateAsync(Test test)
    {
        await _dbContext.Tests.AddAsync(test);
        await _dbContext.SaveChangesAsync();

        return await _dbContext.Tests.FirstOrDefaultAsync(t => t.Id == test.Id);
    }

    public async Task RemoveAsync(int id)
    {
        var test = await _dbContext.Tests.FirstOrDefaultAsync(t => t.Id == id);
        _dbContext.Tests.Remove(test);
    }

    public async Task UpdateAsync(Test test)
    {
        _dbContext.Tests.Update(test);
    }
}