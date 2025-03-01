using LocoTesting.Application.Interfaces;
using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Infrastructure.DataAccess;
using LocoTesting.Infrastructure.Repositories;

namespace LocoTesting.Infrastructure;

public class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public ITestRepository Tests { get; }
    public IOptionRepository Options { get; }
    public IQuestionRepository Questions { get; }
    public IUserRepository Users { get; }

    public UnitOfWork(ApplicationDbContext context, 
        ITestRepository tests, 
        IOptionRepository options, 
        IQuestionRepository questions,
        IUserRepository users)
    {
        _context = context;
        Tests = tests;
        Options = options;
        Questions = questions;
        Users = users;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}