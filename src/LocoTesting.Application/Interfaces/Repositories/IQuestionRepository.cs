using LocoTesting.Domain.Models;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface IQuestionRepository
{
    Task<List<Question>?> GetQuestionsAsync(int testId);
    Task<bool> CheckQuestionExistsAsync(int id);
    Task<Question?> CreateQuestionAsync(Question question);
}