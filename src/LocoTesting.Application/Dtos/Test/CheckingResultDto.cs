using LocoTesting.Application.Dtos.Answer;
using LocoTesting.Application.Dtos.Option;

namespace LocoTesting.Application.Dtos.Test;

public class CheckingResultDto
{
    public int TestId { get; set; }
    public int TotalCorrectAnswers { get; set; }
    /// <summary>
    /// Contains question id and its correct answer
    /// </summary>
    public List<AnswerDto> CorrectAnswers { get; set; }
    public Dictionary<int, OptionResponseDto> CorrectOptions { get; set; } = new Dictionary<int, OptionResponseDto>();
}