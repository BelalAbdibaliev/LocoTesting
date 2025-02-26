using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Question;
using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocoTesting.API.Controllers;

[ApiController]
[Route("tests-admin/")]
public class AdminController: ControllerBase
{
    private readonly ILogger<AdminController> _logger;
    private readonly ITestService _testService;

    public AdminController(ILogger<AdminController> logger, ITestService testService)
    {
        _logger = logger;
        _testService = testService;
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AddAsync([FromBody] CreateTestDto createTestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var createdTest = await _testService.AddTestAsync(createTestDto);
        return Ok(createdTest);
    }

    [HttpPost("addquestion")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AddQuestionAsync([FromBody] CreateQuestionDto createQuestionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var createdQuestion = await _testService.AddQuestionAsync(createQuestionDto);
        return Ok(createdQuestion);
    }

    [HttpPost("addoption")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AddOptionAsync([FromBody] CreateAnswerOptionDto createAnswerOptionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var createdAnswer = await _testService.AddOptionAsync(createAnswerOptionDto);
        return Ok(createdAnswer);
    }
}