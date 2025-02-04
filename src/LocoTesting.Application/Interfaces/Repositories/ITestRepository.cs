using LocoTesting.Domain.Models;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface ITestRepository
{
    Task<List<Test>?> GetAllTestsAsync();
    Task<Test?> GetTestByIdAsync(int id);
    Task<bool> CheckTestExistsAsync(int id);
    Task<bool> CheckQuestionExistsAsync(int id);
    Task<bool> CheckIsTrueAnswerExists(int questionId);
    Task<Test?> CreateTestAsync(Test test);
    Task<Question?> CreateQuestionAsync(Question question);
    Task<Answer?> CreateAnswerAsync(Answer answer);
    Task<List<Question>?> GetQuestionsAsync(int testId);
}