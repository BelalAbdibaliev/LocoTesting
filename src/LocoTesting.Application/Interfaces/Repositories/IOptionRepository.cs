using LocoTesting.Domain.Entities;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface IOptionRepository
{
    Task<AnswerOption> GetCorrectAnswerOptionAsync(int questionId);
    Task<bool> IsTrueAnswerOptionExistsAsync(int questionId);
    Task<AnswerOption?> CreateAsync(AnswerOption answerOption);
}