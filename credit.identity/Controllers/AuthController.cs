using credit.identity.Dtos;
using credit.identity.Services;
using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize(Roles = "Admin")]
    [HttpPost("roles")]
    public async Task<IActionResult> CreateRole(
        CreateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var role = await authService.CreateRole(
            request,
            cancellationToken);

        return Ok(role);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("user-roles")]
    public async Task<IActionResult> AssignRole(
        AssignRoleRequest request,
        CancellationToken cancellationToken)
    {
        await authService.AssignRole(
            request,
            cancellationToken);

        return Ok();
    }
}