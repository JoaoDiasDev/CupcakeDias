using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Dtos;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        var createdUser = await userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await userService.GetUserByIdAsync(id);
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
}