using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Dtos;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        var createdUser = await userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.UserId }, createdUser);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var user = await userService.GetUserByIdAsync(userId);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await userService.GetUserByEmailAsync(email);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var token = await userService.AuthenticateAsync(request.Email, request.Password);

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(new { Token = token });
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refreshRequest)
    {
        // Validate the refresh token
        var user = await userService.GetUserByRefreshTokenAsync(refreshRequest.RefreshToken!);

        if (user == null)
        {
            return Unauthorized("Invalid refresh token.");
        }

        // Generate a new JWT
        var newToken = await userService.GenerateJwtAndRefreshTokens(user);

        return Ok(new { Token = newToken });
    }

}