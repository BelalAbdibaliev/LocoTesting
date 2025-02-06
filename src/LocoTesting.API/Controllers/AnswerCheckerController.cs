using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocoTesting.API.Controllers;

[ApiController]
[Route("answerchecker")]
public class AnswerCheckerController: ControllerBase
{
    private readonly ILogger<AnswerCheckerController> _logger;
    private readonly IAnswerChecker _answerChecker;
    
    public AnswerCheckerController(ILogger<AnswerCheckerController> logger, IAnswerChecker answerChecker)
    {
        _logger = logger;
        _answerChecker = answerChecker;
    }

    [HttpPost]
    public async Task<IActionResult> CheckAnswersAsync([FromBody] CheckAnswerDto checkAnswerDto)
    {
        var result = await _answerChecker.CheckAnswersAsync(checkAnswerDto);
        
        return Ok(result);
    }
}