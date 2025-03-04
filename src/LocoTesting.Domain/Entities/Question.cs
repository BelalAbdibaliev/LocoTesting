namespace LocoTesting.Domain.Entities;

public class Question
{
    public int Id { get; set; }
    public string? Text { get; set; } = string.Empty;
    public string? Content { get; set; } = string.Empty;
    
    public List<AnswerOption>? AnswerOptions { get; set; }
    public int TestId { get; set; }
    public Test? Test { get; set; }
}