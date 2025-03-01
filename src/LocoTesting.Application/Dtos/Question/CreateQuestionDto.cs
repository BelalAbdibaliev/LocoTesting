using System.ComponentModel.DataAnnotations;
using LocoTesting.Application.Dtos.Option;

namespace LocoTesting.Application.Dtos.Question;

public class CreateQuestionDto
{
    [Required]
    public int TestId { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string Question { get; set; }
    
    public string Content { get; set; }
}