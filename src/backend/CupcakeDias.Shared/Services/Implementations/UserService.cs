using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Consts;
using CupcakeDias.Shared.Services.Interfaces;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CupcakeDias.Shared.Services.Implementations;

public class UserService(CupcakeDiasContext context) : IUserService
{

    public async Task<User> CreateUserAsync(User user)
    {
        // Ensure the role exists
        var role = await context.Roles.FindAsync(user.RoleId) ?? throw new Exception("Invalid role specified");
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    // Get user by ID
    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        return (await context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == userId))!;
    }

    // Get user by email
    public async Task<User> GetUserByEmailAsync(string email)
    {
        return (await context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email))!;
    }

    public async Task<string> AuthenticateAsync(string email, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
        {
            return string.Empty;  // Invalid email or password
        }

        // Generate JWT
        var token = await GenerateJwtToken(user);
        return token;
    }

    // Verify the password hash
    private static bool VerifyPasswordHash(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    // Generate JWT token
    private async Task<string> GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(DotEnv.Read()["JWT_SECRET_KEY"]);

        user.Role ??= await GetRoleAsync(user.RoleId);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role?.RoleName!),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private async Task<Role> GetRoleAsync(Guid roleId)
    {
        return await context.Roles.FindAsync(roleId) ?? new Role { RoleName = RoleNames.User };
    }
}
