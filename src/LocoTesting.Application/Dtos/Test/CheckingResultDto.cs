using LocoTesting.Application.Dtos.Option;

namespace LocoTesting.Application.Dtos.Test;

public class CheckingResultDto
{
    public int QuestionId { get; set; }
    public int TotalCorrectAnswers { get; set; }
    public Dictionary<int, bool> AnswerResults { get; set; } = new Dictionary<int, bool>();
    public Dictionary<int, OptionDto> CorrectAnswers { get; set; } = new Dictionary<int, OptionDto>();
}