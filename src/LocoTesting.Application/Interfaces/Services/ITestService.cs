using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Question;
using LocoTesting.Application.Dtos.Test;

namespace LocoTesting.Application.Interfaces.Services;

public interface ITestService
{
    Task<List<TestDto>> GetAllTestsAsync();
    Task<List<QuestionResponseDto>?> GetAllQuestionsAsync(int testId);
    Task<TestDto> AddTestAsync(CreateTestDto dto);
    Task<QuestionResponseDto> AddQuestionAsync(CreateQuestionDto dto);
    Task<AnswerOptionResponseDto> AddOptionAsync(CreateAnswerOptionDto dto);
}