using LocoTesting.Domain.Models;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface IOptionRepository
{
    Task<AnswerOption> GetCorrectAnswerOptionAsync(int questionId);
    Task<bool> IsTrueAnswerOptionExistsAsync(int questionId);
    Task<AnswerOption?> CreateAnswerOptionAsync(AnswerOption answerOption);
}