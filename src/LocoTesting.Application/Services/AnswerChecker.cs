﻿using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Application.Interfaces.Services;

namespace LocoTesting.Application.Services;

public class AnswerChecker: IAnswerChecker
{
    private readonly ITestRepository _testRepository;

    public AnswerChecker(ITestRepository testRepository)
    {
        _testRepository = testRepository;
    }
    
    public async Task<CheckingResultDto> CheckAnswersAsync(CheckAnswerDto checkAnswerDto)
    {
        var totalCorrectAnswers = 0;
        CheckingResultDto checkingResultDto = new CheckingResultDto();

        foreach (var answer in checkAnswerDto.Answers)
        {
            var option = await _testRepository.GetCorrectOptionAsync(answer.QuestionId);
            if (option == null)
                throw new ApplicationException($"Option with id {answer.QuestionId} not found");

            if (option.Id == answer.OptionId)
            {
                totalCorrectAnswers++;
                checkingResultDto.CorrectAnswers.Add(answer);
            }
        }
        checkingResultDto.TotalCorrectAnswers = totalCorrectAnswers;
        return checkingResultDto;
    }
}