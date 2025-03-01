using LocoTesting.Domain.Entities;

namespace LocoTesting.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<UserProfile?> GetUserProfileAsync(string userId);
}