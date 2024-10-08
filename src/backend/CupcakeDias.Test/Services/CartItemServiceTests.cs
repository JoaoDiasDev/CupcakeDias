using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Test.Services;

public class CartItemServiceTests
{
    private readonly CartItemService _cartItemService;
    private readonly CupcakeDiasContext _context;

    public CartItemServiceTests()
    {
        var options = new DbContextOptionsBuilder<CupcakeDiasContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new CupcakeDiasContext(options);
        _cartItemService = new CartItemService(_context);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task AddItemAsync_ShouldAddItemToCart()
    {
        // Arrange
        var item = new CartItem { CartId = Guid.NewGuid(), CupcakeId = Guid.NewGuid(), Quantity = 2, Price = 5.00m };

        // Act
        await _cartItemService.AddItemAsync(item);

        // Assert
        var savedItem = await _context.CartItems.FirstOrDefaultAsync();
        Assert.NotNull(savedItem);
        Assert.Equal(item.Price, savedItem.Price);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetCartItemsAsync_ShouldReturnItemsForUser()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var items = new List<CartItem>
        {
            new CartItem { CartId = cartId, CupcakeId = Guid.NewGuid(), Quantity = 2, Price = 5.00m },
            new CartItem { CartId = cartId, CupcakeId = Guid.NewGuid(), Quantity = 1, Price = 7.50m }
        };
        _context.CartItems.AddRange(items);
        await _context.SaveChangesAsync();

        // Act
        var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cartId);

        // Assert
        var enumerable = cartItems.ToList();
        Assert.Equal(2, enumerable.Count());
        Assert.Equal(5.00m, enumerable.First().Price);
    }
}
