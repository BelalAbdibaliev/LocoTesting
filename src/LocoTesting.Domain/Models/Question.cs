﻿namespace LocoTesting.Domain.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string? Content { get; set; }
    
    public List<Option>? Options { get; set; }
    public int TestId { get; set; }
    public Test? Test { get; set; }
}