using System.ComponentModel.DataAnnotations;

namespace LocoTesting.Application.Dtos.Test;

public class CreateQuestionDto
{
    [Required]
    public int TestId { get; set; }
    
    [Required]
    public string Question { get; set; }
    
    public string Content { get; set; }
    
    public List<AnswerDto> AnswersDto { get; set; } = new List<AnswerDto>();
}