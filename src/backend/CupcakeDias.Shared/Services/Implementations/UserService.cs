using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using dotenv.net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CupcakeDias.Shared.Services.Implementations;

public class UserService(CupcakeDiasContext context) : IUserService
{

    public async Task<User> CreateUserAsync(User user)
    {
        // Ensure the role exists
        var role = await context.Roles.FindAsync(user.RoleId) ?? throw new Exception("Invalid role specified");

        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    // Get user by ID
    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        return await context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == userId);
    }

    // Get user by email
    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<string> AuthenticateAsync(string email, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
        {
            return string.Empty;  // Invalid email or password
        }

        // Generate JWT
        var token = GenerateJwtToken(user);
        return token;
    }

    // Verify the password hash
    private bool VerifyPasswordHash(string password, string storedHash)
    {
        // For simplicity, use direct comparison in this example
        return password.Equals(storedHash);
    }

    // Generate JWT token
    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(DotEnv.Read()["JWT_SECRET_KEY"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user?.Role?.RoleName!)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
