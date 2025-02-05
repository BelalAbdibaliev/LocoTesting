using LocoTesting.Application.Dtos.Answer;
using LocoTesting.Application.Dtos.Question;
using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocoTesting.API.Controllers;

[ApiController]
[Route("tests-admin/")]
public class AdminTestController: ControllerBase
{
    private readonly ILogger<AdminTestController> _logger;
    private readonly ITestService _testService;

    public AdminTestController(ILogger<AdminTestController> logger, ITestService testService)
    {
        _logger = logger;
        _testService = testService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> AddAsync([FromBody] CreateTestDto createTestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var createdTest = await _testService.AddTestAsync(createTestDto);
        return Ok(createdTest);
    }

    [HttpPost("addquestion")]
    public async Task<ActionResult> AddQuestionAsync([FromBody] CreateQuestionDto createQuestionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var createdQuestion = await _testService.AddQuestionAsync(createQuestionDto);
        return Ok(createdQuestion);
    }

    [HttpPost("addanswer")]
    public async Task<ActionResult> AddAnswerAsync([FromBody] CreateAnswerDto createAnswerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var createdAnswer = await _testService.AddAnswerAsync(createAnswerDto);
        return Ok(createdAnswer);
    }
}