using LocoTesting.Application.Dtos.Test;

namespace LocoTesting.Application.Interfaces;

public interface ITestService
{
    Task<List<TestDto>> GetAllTestsAsync();
    Task<TestDto> CreateTestAsync(CreateTestDto dto);
    Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto dto);
}