using Core.Entities.ReturnModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoProject.Model.Tokens.Dtos.Response;
using ToDoProject.Model.Users.Dtos.Request;
using ToDoProject.Service.Authentication.Abstracts;

namespace ToDoProject.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationsController(IAuthenticationService authService) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var tokenResponse = await authService.RegisterByTokenAsync(request);
        if (tokenResponse == null)
        {
            var errorResponse = ReturnModel<TokenResponseDto>.Failure("Registration failed.");
            return BadRequest(errorResponse);
        }

        var response = ReturnModel<TokenResponseDto>.Success(tokenResponse);
        return Ok(response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var tokenResponse = await authService.LoginByTokenAsync(request);
        if (tokenResponse == null)
        {
            var errorResponse = ReturnModel<TokenResponseDto>.Failure("Invalid username or password.");
            return Unauthorized(errorResponse);
        }

        var response = ReturnModel<TokenResponseDto>.Success(tokenResponse);
        return Ok(response);
    }
}
