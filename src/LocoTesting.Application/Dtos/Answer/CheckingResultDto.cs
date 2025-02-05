namespace LocoTesting.Application.Dtos.Answer;

public class CheckingResultDto
{
    public int QuestionId { get; set; }
    public int CorrectAnswers { get; set; }
    public Dictionary<int, bool> AnswerResults { get; set; } = new Dictionary<int, bool>();
}