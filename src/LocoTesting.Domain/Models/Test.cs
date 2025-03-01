﻿namespace LocoTesting.Domain.Models;

public class Test
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public List<Question> Questions { get; set; } = new List<Question>();
}