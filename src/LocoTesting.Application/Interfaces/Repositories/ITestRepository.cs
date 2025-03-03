using LocoTesting.Application.Dtos.Test;
using LocoTesting.Domain.Entities;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface ITestRepository
{
    Task<List<Test>?> GetAllAsync();
    Task<Test?> GetByIdAsync(int id);
    Task<bool> IsExistsAsync(int id);
    Task<Test?> CreateAsync(Test test);
    Task RemoveAsync(int id);
    Task UpdateAsync(Test newValues);
}