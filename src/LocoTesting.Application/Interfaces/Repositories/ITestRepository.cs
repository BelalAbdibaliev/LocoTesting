using LocoTesting.Application.Dtos.Test;
using LocoTesting.Domain.Entities;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface ITestRepository
{
    Task<bool> IsExistsAsync(int id);
}