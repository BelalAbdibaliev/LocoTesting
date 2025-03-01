using LocoTesting.Domain.Entities;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface IQuestionRepository
{
    Task<List<Question>?> GetQuestionsAsync(int testId);
    Task<bool> IsQuestionExistsAsync(int id);
    Task<Question?> CreateQuestionAsync(Question question);
}