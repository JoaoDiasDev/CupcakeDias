using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Consts;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CupcakeDias.Shared.Services.Implementations;

public class UserService(CupcakeDiasContext context) : IUserService
{
    /// <summary>
    /// All new users are created with the User role and password is hashed
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<User> CreateUserAsync(User user)
    {
        var role = await context.Roles.FirstOrDefaultAsync(r => r.RoleName.Equals(RoleNames.User));
        user.Role = role;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    // Get user by ID
    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        return (await context.Users.AsNoTracking().Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == userId))!;
    }

    // Get user by email
    public async Task<User> GetUserByEmailAsync(string email)
    {
        return (await context.Users.AsNoTracking().Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email))!;
    }

    public async Task<string> AuthenticateAsync(string email, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
        {
            return string.Empty;
        }

        // Generate JWT
        var (token, _) = await GenerateJwtAndRefreshTokens(user);
        return token;
    }

    // Verify the password hash
    private static bool VerifyPasswordHash(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    // Generate JWT token and refresh token
    public async Task<(string jwtToken, string refreshToken)> GenerateJwtAndRefreshTokens(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT__SECRET") ?? "NotSoSecret");

        user.Role ??= await GetRoleAsync(user.RoleId);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role?.RoleName!),
        ]),
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(jwtToken);

        // Generate a refresh token
        var refreshToken = await GenerateRefreshTokenAsync(user);

        return (tokenString, refreshToken);
    }


    public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task<string> GenerateRefreshTokenAsync(User user)
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        var refreshToken = Convert.ToBase64String(randomNumber); // Create a Base64 encoded string

        user.RefreshToken = refreshToken;

        await context.SaveChangesAsync(); // Save the user object with the refresh token and expiry to the database

        return refreshToken;
    }


    private async Task<Role> GetRoleAsync(Guid roleId)
    {
        return await context.Roles.FindAsync(roleId) ?? new Role { RoleName = RoleNames.User };
    }


}
