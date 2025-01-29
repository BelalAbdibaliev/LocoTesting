﻿namespace LocoTesting.Application.Dtos.Test;

public class QuestionDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Content { get; set; }
    public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
    
    public int TestId { get; set; }
}