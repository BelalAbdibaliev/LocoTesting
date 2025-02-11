using LocoTesting.Application.Dtos.Answer;
using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Test;

namespace LocoTesting.Application.Interfaces.Services;

public interface IAnswerChecker
{
    Task<CheckingResultDto> CheckAnswersAsync(CheckAnswerDto checkAnswerDto);
}