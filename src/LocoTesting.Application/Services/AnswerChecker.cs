using LocoTesting.Application.Dtos.Answer;
using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces;
using LocoTesting.Application.Interfaces.Repositories;
using LocoTesting.Application.Interfaces.Services;

namespace LocoTesting.Application.Services;

public class AnswerChecker: IAnswerChecker
{
    private readonly IUnitOfWork _unitOfWork;

    public AnswerChecker(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CheckingResultDto> CheckAnswersAsync(CheckAnswerDto checkAnswerDto)
    {
        var totalCorrectAnswers = 0;
        CheckingResultDto checkingResultDto = new CheckingResultDto();

        foreach (var answer in checkAnswerDto.Answers)
        {
            var correctAnswer = await _unitOfWork.Options.GetCorrectOptionAsync(answer.QuestionId);
            if (correctAnswer == null)
                throw new ApplicationException($"Option with id {answer.QuestionId} not found");

            CorrectAnswerDto correctAnswerDto = new CorrectAnswerDto
            {
                QuestionId = correctAnswer.QuestionId,
                CorrectAnswerId = correctAnswer.Id,
                Answer = correctAnswer.Text
            };
            
            checkingResultDto.ExpectedAnswers.Add(correctAnswerDto);

            if (correctAnswer.Id == answer.OptionId)
            {
                totalCorrectAnswers++;
                checkingResultDto.SubmittedCorrectAnswers.Add(answer);
            }
        }
        checkingResultDto.TotalCorrectAnswers = totalCorrectAnswers;
        return checkingResultDto;
    }
}