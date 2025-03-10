﻿using System.ComponentModel.DataAnnotations;

namespace LocoTesting.Application.Dtos.Answer;

public class CheckAnswerDto
{
    [Required]
    public int TestId { get; set; }
    public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
}