using System;
using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Test.Services;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly CupcakeDiasContext _context;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<CupcakeDiasContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new CupcakeDiasContext(options);
        _userService = new UserService(_context);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task CreateUserAsync_ShouldAddUserToDatabase()
    {
        // Arrange
        var user = new User
        {
            Email = "pP7rF@example.com",
            Name = "John Doe",
            Address = "123 Main St",
            PasswordHash = "passwordHash",
            PhoneNumber = "+1234567890"
        };

        // Act
        var createdUser = await _userService.CreateUserAsync(user);

        // Assert
        var savedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == "pP7rF@example.com");
        Assert.NotNull(savedUser);
        Assert.Equal("John Doe", savedUser.Name);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetUserByIdAsync_ShouldReturnCorrectUser()
    {
        // Arrange
        var user = new User
        {
            Email = "pP7rF@example.com",
            Name = "John Doe",
            Address = "123 Main St",
            PasswordHash = "passwordHash",
            PhoneNumber = "+1234567890"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var retrievedUser = await _userService.GetUserByIdAsync(user.UserId);

        // Assert
        Assert.NotNull(retrievedUser);
        Assert.Equal(user.UserId, retrievedUser.UserId);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetUserByEmailAsync_ShouldReturnCorrectUser()
    {
        // Arrange
        var user = new User
        {
            Email = "pP7rF@example.com",
            Name = "John Doe",
            Address = "123 Main St",
            PasswordHash = "passwordHash",
            PhoneNumber = "+1234567890"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var retrievedUser = await _userService.GetUserByEmailAsync("pP7rF@example.com");

        // Assert
        Assert.NotNull(retrievedUser);
        Assert.Equal(user.Email, retrievedUser.Email);
    }
}