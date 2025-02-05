using LocoTesting.Application.Dtos.Answer;
using LocoTesting.Application.Interfaces.Services;

namespace LocoTesting.Application.Services;

public class AnswerChecker: IAnswerChecker
{
    public async Task<CheckingResultDto> CheckAnswerAsync(CheckAnswerDto checkAnswerDto)
    {
        throw new NotImplementedException();
    }
}