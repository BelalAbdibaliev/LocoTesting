using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocoTesting.API.Controllers;

[ApiController]
[Route("testing")]
public class Testing:ControllerBase
{
    [HttpGet("Test")]
    [Authorize]
    public IActionResult Get()
    {
        return Ok();
    }
}