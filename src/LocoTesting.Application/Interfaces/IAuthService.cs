using LocoTesting.Application.Dtos.Auth;

namespace LocoTesting.Application.Interfaces;

public interface IAuthService
{
    Task<GoogleAuthResponseDto> GoogleAuth(GoogleAuthDto googleAuthDto);
}