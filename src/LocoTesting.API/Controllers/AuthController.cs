﻿using Google.Apis.Auth;
using LocoTesting.Application.Dtos.Auth;
using LocoTesting.Application.Interfaces;
using LocoTesting.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocoTesting.API.Controllers;

[Route("auth/")]
[ApiController]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("google-auth")]
    public async Task<IActionResult> GoogleAuth([FromBody] GoogleAuthDto googleAuthDto)
    {
        return Ok(await _authService.GoogleAuth(googleAuthDto));
    }
}