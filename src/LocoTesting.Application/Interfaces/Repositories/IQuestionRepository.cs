using LocoTesting.Domain.Entities;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface IQuestionRepository
{
    Task<List<Question>?> GetByTestIdAsync(int testId);
    Task<bool> IsExistsAsync(int id);
}