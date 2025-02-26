using LocoTesting.Application.Interfaces.Services;
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
        return Ok(await _testService.GetAllTestsAsync());
    }
    
    [HttpGet("getallquestions")]
    [Authorize]
    public async Task<ActionResult> GetAllQuestionsAsync([FromQuery] int testId)
    {
        var questions = await _testService.GetAllQuestionsAsync(testId);
        return Ok(questions);
    }
}