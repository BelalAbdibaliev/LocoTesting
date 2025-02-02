using LocoTesting.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocoTesting.API.Controllers;

[ApiController]
[Route("tests-user")]
public class UserTestController : ControllerBase
{
    private readonly ILogger<UserTestController> _logger;
    private readonly ITestService _testService;
    
    public UserTestController(ITestService testService, ILogger<UserTestController> logger)
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
    public async Task<ActionResult> GetAllQuestionsAsync([FromQuery] int testId)
    {
        var questions = await _testService.GetAllQuestionsAsync(testId);
        return Ok(questions);
    }
}