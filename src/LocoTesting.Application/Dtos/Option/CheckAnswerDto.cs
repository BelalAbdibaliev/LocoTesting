using LocoTesting.Application.Dtos.Answer;

namespace LocoTesting.Application.Dtos.Option;

public class CheckAnswerDto
{
    public int TestId { get; set; }
    /// <summary>
    /// Contains question id and answer id
    /// </summary>
    public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
}