using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces;
using LocoTesting.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocoTesting.API.Controllers;

[ApiController]
[Route("tests/")]
public class TestController: ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly ITestService _testService;

    public TestController(ILogger<TestController> logger, ITestService testService)
    {
        _logger = logger;
        _testService = testService;
    }
    
    [HttpGet("getall")]
    public async Task<ActionResult> GetAllAsync()
    {
        return Ok(await _testService.GetAllTestsAsync());
    }

    [HttpPost("create")]
    public async Task<ActionResult> AddAsync([FromBody] CreateTestDto createTestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var createdTest = await _testService.CreateTestAsync(createTestDto);
        return Ok(createdTest);
    }

    [HttpPost("addquestion")]
    public async Task<ActionResult> AddQuestionAsync([FromBody] CreateQuestionDto createQuestionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var createdQuestion = await _testService.CreateQuestionAsync(createQuestionDto);
        return Ok(createdQuestion);
    }
}