using credit.identity.Dtos;
using credit.identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace credit.identity.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(
            await authService.AuthenticateAsync(
                request,
                cancellationToken));
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