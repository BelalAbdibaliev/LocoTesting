using LocoTesting.Application.Dtos.Auth;

namespace LocoTesting.Application.Interfaces.Services;

public interface IAuthService
{
    Task<GoogleAuthResponseDto> GoogleAuth(GoogleAuthDto googleAuthDto);
}