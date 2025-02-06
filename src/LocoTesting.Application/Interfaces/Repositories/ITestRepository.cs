using LocoTesting.Domain.Models;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface ITestRepository
{
    Task<List<Test>?> GetAllTestsAsync();
    Task<List<Question>?> GetQuestionsAsync(int testId);
    Task<Test?> GetTestByIdAsync(int id);
    Task<Option> GetCorrectOptionAsync(int questionId);
    Task<bool> CheckTestExistsAsync(int id);
    Task<bool> CheckQuestionExistsAsync(int id);
    Task<bool> CheckIsTrueOptionExists(int questionId);
    Task<Test?> CreateTestAsync(Test test);
    Task<Question?> CreateQuestionAsync(Question question);
    Task<Option?> CreateOptionAsync(Option option);
}