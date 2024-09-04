using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Test.Services;

public class CartServiceTests
{
    private readonly CartService _cartService;
    private readonly CupcakeDiasContext _context;

    public CartServiceTests()
    {
        var options = new DbContextOptionsBuilder<CupcakeDiasContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new CupcakeDiasContext(options);
        _cartService = new CartService(_context);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task CreateCartAsync_ShouldAddCartToDatabase()
    {
        // Arrange
        var cart = new Cart { UserId = Guid.NewGuid(), Status = "Active" };

        // Act
        await _cartService.CreateCartAsync(cart);

        // Assert
        var savedCart = await _context.Carts.FirstOrDefaultAsync();
        Assert.NotNull(savedCart);
        Assert.Equal(cart.UserId, savedCart.UserId);
        Assert.Equal(cart.Status, savedCart.Status);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetCartByIdAsync_ShouldReturnCartWithItems()
    {
        // Arrange
        var cart = new Cart
        {
            UserId = Guid.NewGuid(),
            Status = "Active",
            CartItems =
            [
                new CartItem { CupcakeId = Guid.NewGuid(), Quantity = 2, Price = 5.00m },
                new CartItem { CupcakeId = Guid.NewGuid(), Quantity = 1, Price = 7.50m }
            ]
        };
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        // Act
        var retrievedCart = await _cartService.GetCartByIdAsync(cart.CartId);

        // Assert
        Assert.NotNull(retrievedCart);
        Assert.Equal(2, retrievedCart?.CartItems?.Count);
        Assert.Equal(5.00m, retrievedCart?.CartItems?.First().Price);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetCartsByUserIdAsync_ShouldReturnCartsForUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var carts = new List<Cart>
        {
            new() { UserId = userId, Status = "Active" },
            new() { UserId = userId, Status = "Pending" }
        };
        _context.Carts.AddRange(carts);
        await _context.SaveChangesAsync();

        // Act
        var userCarts = await _cartService.GetCartsByUserIdAsync(userId);

        // Assert
        var enumerable = userCarts.ToList();
        Assert.Equal(2, enumerable.Count());
        Assert.Equal("Active", enumerable.First().Status);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task UpdateCartAsync_ShouldUpdateCartInDatabase()
    {
        // Arrange
        var cart = new Cart { UserId = Guid.NewGuid(), Status = "Active" };
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        // Act
        cart.Status = "Pending";
        await _cartService.UpdateCartAsync(cart);

        // Assert
        var updatedCart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId.Equals(cart.CartId));
        Assert.NotNull(updatedCart);
        Assert.Equal("Pending", updatedCart.Status);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task DeleteCartAsync_ShouldRemoveCartFromDatabase()
    {
        // Arrange
        var cart = new Cart { UserId = Guid.NewGuid(), Status = "Active" };
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        // Act
        await _cartService.DeleteCartAsync(cart.CartId);

        // Assert
        var deletedCart = await _context.Carts.FindAsync(cart.CartId);
        Assert.Null(deletedCart);
    }
}
