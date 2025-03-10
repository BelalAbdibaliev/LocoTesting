﻿using Google.Apis.Auth;
using LocoTesting.Application.Dtos.Auth;
using LocoTesting.Application.Interfaces;
using LocoTesting.Application.Interfaces.Services;
using LocoTesting.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LocoTesting.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ILogger<AuthService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(UserManager<AppUser> userManager, 
        ITokenGenerator tokenGenerator,
        ILogger<AuthService> logger,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<GoogleAuthResponseDto> GoogleAuth(GoogleAuthDto googleAuthDto)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(googleAuthDto.GoogleToken);

        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user == null)
        {
            user = await CreateUserAsync(googleAuthDto);
        }
        
        var userProfile = await _unitOfWork.Users.GetUserProfileAsync(user.Id);
        
        var roles = await _userManager.GetRolesAsync(user);
        _logger.LogInformation($"Successfully logged in {user.UserName}");
        return new GoogleAuthResponseDto
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = userProfile.FirstName,
            LastName = userProfile.LastName,
            Email = user.Email,
            Token = _tokenGenerator.GenerateToken(user, roles)
        };
    }

    public async Task<AppUser> CreateUserAsync(GoogleAuthDto googleAuthDto)
    {
        if(googleAuthDto is null)
            throw new ArgumentNullException("User cannot be null");
        
        var payload = await GoogleJsonWebSignature.ValidateAsync(googleAuthDto.GoogleToken);
        var user = new AppUser
        {
            UserName = googleAuthDto.UniqueUserName,
            Email = payload.Email,
            UserProfile = new UserProfile
            {
                FirstName = googleAuthDto.FirstName,
                LastName = googleAuthDto.LastName,
            }
        };
            
        var creationResult = await _userManager.CreateAsync(user);
        if(!creationResult.Succeeded)
            throw new DbUpdateException("Registration failed");
            
        var rolesResult = await _userManager.AddToRoleAsync(user, "User");
        if(!rolesResult.Succeeded)
            throw new DbUpdateException("Failed to assign role to user");
        return user;
    }
}