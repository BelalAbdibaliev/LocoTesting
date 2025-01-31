using System.ComponentModel.DataAnnotations;

namespace LocoTesting.Application.Dtos.Test;

public class CreateAnswerDto
{
    [Required]
    public int QuestionId { get; set; }
    
    [Required]
    public string Text { get; set; }
    [Required]
    public bool IsCorrect { get; set; }
}