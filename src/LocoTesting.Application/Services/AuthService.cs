using Google.Apis.Auth;
using LocoTesting.Application.Dtos.Auth;
using LocoTesting.Application.Interfaces.Services;
using LocoTesting.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LocoTesting.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserManager<AppUser> userManager, 
        ITokenGenerator tokenGenerator,
        ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
    }

    public async Task<GoogleAuthResponseDto> GoogleAuth(GoogleAuthDto googleAuthDto)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(googleAuthDto.GoogleToken);

        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user == null)
        {
            user = new AppUser
            {
                UserName = googleAuthDto.UniqueUserName,
                Email = payload.Email,
                FirstName = googleAuthDto.FirstName,
                LastName = googleAuthDto.LastName,
            };
            
            var creationResult = await _userManager.CreateAsync(user);
            if(!creationResult.Succeeded)
                throw new DbUpdateException("Registration failed");
            
            var rolesResult = await _userManager.AddToRoleAsync(user, "User");
            if(!rolesResult.Succeeded)
                throw new DbUpdateException("Failed to assign role to user");
        }
        
        var roles = await _userManager.GetRolesAsync(user);
        _logger.LogInformation($"Successfully logged in {user.UserName}");
        return new GoogleAuthResponseDto
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Token = _tokenGenerator.GenerateToken(user, roles)
        };
    }
}