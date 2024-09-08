using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> GetUserByEmailAsync(string email);
    Task<string> AuthenticateAsync(string email, string password);
    Task<(string jwtToken, string refreshToken)> GenerateJwtAndRefreshTokens(User user);
    Task<string> GenerateRefreshTokenAsync(User user);
    Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
}
