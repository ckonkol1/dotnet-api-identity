using Identity.Interfaces;
using Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController(ITokenService tokenService) : ControllerBase
{
    [HttpPost("token")]
    [Produces("application/json")]
    public IActionResult GenerateToken([FromBody] TokenGenerationRequest request)
    {
        var jwt = tokenService.GenerateToken(request);
        return Ok(jwt);
    }
}