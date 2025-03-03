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

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _dbContext.Tests.AnyAsync(t => t.Id == id);
    }
}