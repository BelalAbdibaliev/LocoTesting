using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces.Services;

namespace LocoTesting.Application.Services;

public class AnswerChecker: IAnswerChecker
{
    public async Task<CheckingResultDto> CheckAnswerAsync(CheckAnswerDto checkAnswerDto)
    {
        throw new NotImplementedException();
    }
}