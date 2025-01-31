namespace LocoTesting.Application.Dtos.Test;

public class CreateAnswerDto
{
    public int QuestionId { get; set; }
    
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
}