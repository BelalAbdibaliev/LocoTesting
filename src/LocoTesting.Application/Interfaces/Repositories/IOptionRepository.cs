using LocoTesting.Domain.Models;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface IOptionRepository
{
    Task<Option> GetCorrectOptionAsync(int questionId);
    Task<bool> CheckIsTrueOptionExists(int questionId);
    Task<Option?> CreateOptionAsync(Option option);
}