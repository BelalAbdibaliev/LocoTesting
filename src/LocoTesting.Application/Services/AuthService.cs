using System.Data;
using Google.Apis.Auth;
using LocoTesting.Application.Dtos.Auth;
using LocoTesting.Application.Interfaces;
using LocoTesting.Domain.Models;
using LocoTesting.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LocoTesting.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly TokenGeneratorService _tokenGenerator;
    private readonly IEmailValidator _emailValidator;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserManager<AppUser> userManager, 
        TokenGeneratorService tokenGenerator,
        IEmailValidator emailValidator,
        ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
        _emailValidator = emailValidator;
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
                UserName = payload.Name,
                Email = payload.Email,
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