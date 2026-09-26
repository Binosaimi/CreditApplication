using credit.identity.Dtos;
using credit.identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace credit.identity.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var user = await authService.AuthenticateAsync(
            request,
            cancellationToken);

        return Ok(user);
    }
    [HttpPost("register")]
    public async Task<ActionResult<SignupResponse>> Register(
        SignupRequest request,
        CancellationToken cancellationToken)
    {
        var response = await authService.RegisterAsync(
            request,
            cancellationToken);

        return Ok(response);
    }
}