using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Test;

namespace LocoTesting.Application.Interfaces.Services;

public interface IAnswerChecker
{
    Task<CheckingResultDto> CheckAnswerAsync(CheckAnswerDto checkAnswerDto);
}