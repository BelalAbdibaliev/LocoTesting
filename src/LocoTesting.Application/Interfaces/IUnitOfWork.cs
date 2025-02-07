using LocoTesting.Application.Interfaces.Repositories;

namespace LocoTesting.Application.Interfaces;

public interface IUnitOfWork
{
    ITestRepository Tests { get; }
    IOptionRepository Options { get; }
    IQuestionRepository Questions { get; }
    
    Task<int> SaveChangesAsync();
}