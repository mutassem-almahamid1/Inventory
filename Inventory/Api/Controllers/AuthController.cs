using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Features.Auth.Commands.Login;
using Services.Features.Auth.Commands.Logout;
using Services.Features.Auth.Commands.RefreshToken;
using Services.Features.Auth.Commands.Signup;
using Services.Features.Auth.Queries.GetCurrentUser;
using Shared.Requests;
using Shared.Responses;
using System.Security.Claims;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Signup([FromBody] SignupRequest request)
    {
        var result = await sender.Send(new SignupCommand(request));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        var result = await sender.Send(new LoginCommand(request));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> Refresh([FromBody] RefreshTokenRequest request)
    {
        var result = await sender.Send(new RefreshTokenCommand(request,
            HttpContext.Connection.RemoteIpAddress?.ToString(), Request.Headers.UserAgent.ToString()));
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<ActionResult> Logout([FromBody] RefreshTokenRequest request)
    {
        var result =
            await sender.Send(new LogoutCommand(request.RefreshToken,
                HttpContext.Connection.RemoteIpAddress?.ToString()));
        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Error);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<AuthResponse>> Me()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var result = await sender.Send(new GetCurrentUserQuery(userId));
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Error);
    }
}