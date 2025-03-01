using System.ComponentModel.DataAnnotations;

namespace LocoTesting.Application.Dtos.Option;

public class CreateAnswerOptionDto
{
    [Required]
    public int QuestionId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Text { get; set; }
    [Required]
    public bool IsCorrect { get; set; }
}