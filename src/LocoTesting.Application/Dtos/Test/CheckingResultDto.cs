using LocoTesting.Application.Dtos.Answer;
using LocoTesting.Application.Dtos.Option;

namespace LocoTesting.Application.Dtos.Test;

public class CheckingResultDto
{
    public int TestId { get; set; }
    public int TotalCorrectAnswers { get; set; }
    public List<AnswerDto> SubmittedCorrectAnswers { get; set; } = new List<AnswerDto>();
    public List<CorrectAnswerDto> ExpectedAnswers { get; set; } = new List<CorrectAnswerDto>();
}