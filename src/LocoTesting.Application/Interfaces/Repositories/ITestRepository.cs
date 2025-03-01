using LocoTesting.Domain.Entities;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface ITestRepository
{
    Task<List<Test>?> GetAllTestsAsync();
    Task<Test?> GetTestByIdAsync(int id);
    Task<bool> IsTestExistsAsync(int id);
    Task<Test?> CreateTestAsync(Test test);
    Task RemoveTestAsync(int id);
}