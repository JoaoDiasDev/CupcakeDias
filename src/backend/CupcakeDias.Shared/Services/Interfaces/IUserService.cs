using System;
using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> GetUserByEmailAsync(string email);
    Task<string> AuthenticateAsync(string email, string password);
}
