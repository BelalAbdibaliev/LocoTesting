using LocoTesting.Application.Dtos.Answer;

namespace LocoTesting.Application.Interfaces.Services;

public interface IAnswerChecker
{
    Task<CheckingResultDto> CheckAnswerAsync(CheckAnswerDto checkAnswerDto);
}