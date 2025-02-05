using LocoTesting.Application.Dtos.Answer;

namespace LocoTesting.Application.Dtos.Question;

public class QuestionResponseDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Content { get; set; }
    public List<AnswerResponseDto> Answers { get; set; } = new List<AnswerResponseDto>();
    
    public int TestId { get; set; }
}