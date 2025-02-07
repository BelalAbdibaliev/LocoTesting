using LocoTesting.Application.Dtos.Answer;
using LocoTesting.Application.Dtos.Option;

namespace LocoTesting.Application.Dtos.Test;

public class CheckingResultDto
{
    public int TestId { get; set; }
    public int TotalCorrectAnswers { get; set; }
    public List<AnswerDto> CorrectAnswers { get; set; } = new List<AnswerDto>();
    public List<CorrectAnswerDto> CorrectOptions { get; set; } = new List<CorrectAnswerDto>();
}