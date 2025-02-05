using System.ComponentModel.DataAnnotations;
using LocoTesting.Application.Dtos.Option;

namespace LocoTesting.Application.Dtos.Question;

public class CreateQuestionDto
{
    [Required]
    public int TestId { get; set; }
    
    [Required]
    public string Question { get; set; }
    
    public string Content { get; set; }
    
    public List<OptionDto> AnswersDto { get; set; } = new List<OptionDto>();
}