using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Question;
using LocoTesting.Application.Dtos.Test;
using LocoTesting.Application.Interfaces.Services;
using LocoTesting.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocoTesting.API.Controllers;

[ApiController]
[Route("tests-admin/")]
public class AdminController : ControllerBase
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

        await _testService.CreateAsync<Test, CreateTestDto>(createTestDto);
        return Ok();
    }

    [HttpPost("addquestion")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AddQuestionAsync([FromBody] CreateQuestionDto createQuestionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _testService.CreateAsync<Question, CreateQuestionDto>(createQuestionDto);
        return Ok();
    }

    [HttpPost("addoption")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AddOptionAsync([FromBody] CreateAnswerOptionDto createAnswerOptionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _testService.CreateAnswerOptionAsync(createAnswerOptionDto);
        return Ok();
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTestAsync(int id)
    {
        await _testService.DeleteAsync<Test>(id);
        return Ok();
    }

    [HttpPost("update-test")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateTestAsync([FromBody] UpdateTestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _testService.UpdateEntityAsync<Test, UpdateTestDto>(dto);
        
        return Ok();
    }
}