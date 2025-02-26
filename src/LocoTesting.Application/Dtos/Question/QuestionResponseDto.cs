using LocoTesting.Application.Dtos.Option;

namespace LocoTesting.Application.Dtos.Question;

public class QuestionResponseDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Content { get; set; }
    public List<AnswerOptionResponseDto> AnswerOptions { get; set; } = new List<AnswerOptionResponseDto>();
    
    public int TestId { get; set; }
}