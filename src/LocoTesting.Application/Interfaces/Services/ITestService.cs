using LocoTesting.Application.Dtos.Answer;
using LocoTesting.Application.Dtos.Question;
using LocoTesting.Application.Dtos.Test;

namespace LocoTesting.Application.Interfaces.Services;

public interface ITestService
{
    Task<List<TestDto>> GetAllTestsAsync();
    Task<List<QuestionDto>?> GetAllQuestionsAsync(int testId);
    Task<TestDto> AddTestAsync(CreateTestDto dto);
    Task<QuestionDto> AddQuestionAsync(CreateQuestionDto dto);
    Task<AnswerDto> AddAnswerAsync(CreateAnswerDto dto);
}