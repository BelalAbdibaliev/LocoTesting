using LocoTesting.Application.Dtos.Option;

namespace LocoTesting.Application.Dtos.Question;

public class QuestionResponseDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Content { get; set; }
    public List<OptionResponseDto> Options { get; set; } = new List<OptionResponseDto>();
    
    public int TestId { get; set; }
}