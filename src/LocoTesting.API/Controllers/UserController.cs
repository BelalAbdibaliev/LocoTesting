using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces.Services;
using LocoTesting.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocoTesting.API.Controllers;

[ApiController]
[Route("tests-user")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly ITestService _testService;
    
    public UserController(ITestService testService, ILogger<UserController> logger)
    {
        _testService = testService;
        _logger = logger;
    }
    
    [HttpGet("getall")]
    public async Task<ActionResult> GetAllAsync()
    {
        return Ok(await _testService.GetAllAsync<Test>());
    }
    
    [HttpGet("getallquestions")]
    [Authorize]
    public async Task<ActionResult> GetAllQuestionsAsync([FromQuery] int testId)
    {
        var questions = await _testService.GetQuestionsByTestIdAsync(testId);
        return Ok(questions);
    }
}