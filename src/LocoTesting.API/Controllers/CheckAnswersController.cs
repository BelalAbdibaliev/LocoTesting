using LocoTesting.Application.Dtos.Answer;
using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocoTesting.API.Controllers;

[ApiController]
[Route("answerchecker")]
public class CheckAnswersController: ControllerBase
{
    private readonly ILogger<CheckAnswersController> _logger;
    private readonly IAnswerChecker _answerChecker;
    
    public CheckAnswersController(ILogger<CheckAnswersController> logger, IAnswerChecker answerChecker)
    {
        _logger = logger;
        _answerChecker = answerChecker;
    }

    [HttpPost("check")]
    public async Task<IActionResult> CheckAnswersAsync([FromBody] CheckAnswerDto checkAnswerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var result = await _answerChecker.CheckAnswersAsync(checkAnswerDto);
        
        return Ok(result);
    }
}