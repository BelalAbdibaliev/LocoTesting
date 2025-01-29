namespace LocoTesting.Domain.Models;

public class Test
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public List<Question> Questions { get; set; } = new List<Question>();
}