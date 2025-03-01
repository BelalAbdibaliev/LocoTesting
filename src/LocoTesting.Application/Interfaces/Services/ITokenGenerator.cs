using LocoTesting.Domain.Entities;

namespace LocoTesting.Application.Interfaces.Services;

public interface ITokenGenerator
{
    string GenerateToken(AppUser user, IList<string> roles);
}