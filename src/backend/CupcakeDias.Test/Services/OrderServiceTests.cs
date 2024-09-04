using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Consts;
using CupcakeDias.Shared.Services.Implementations;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CupcakeDias.Test.Services;

public class OrderServiceTests
{
    private readonly OrderService _orderService;
    private readonly CupcakeDiasContext _context;
    private readonly Mock<IEmailService> _emailServiceMock;

    public OrderServiceTests()
    {
        var options = new DbContextOptionsBuilder<CupcakeDiasContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        _emailServiceMock = new Mock<IEmailService>();
        _context = new CupcakeDiasContext(options);
        _orderService = new OrderService(_context, _emailServiceMock.Object);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task CreateOrderAsync_ShouldSaveOrderAndSendMessage()
    {
        // Arrange
        var order = new Order
        {
            Status = OrderStatus.Pending,
            User = new User
            {
                Email = "pP7rF@example.com",
                Name = "John Doe",
                Address = "123 Main St",
                PasswordHash = "passwordHash",
                PhoneNumber = "+1234567890",
                RoleId = Guid.NewGuid(),
            },

            OrderDetails =
            [
                new OrderDetail { CupcakeId = Guid.NewGuid(), Quantity = 2, Price = 5.00m }
            ]
        };

        // Act
        var createdOrder = await _orderService.CreateOrderAsync(order);

        // Assert
        var savedOrder = await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync();
        Assert.NotNull(savedOrder);
        Assert.Equal(order.OrderDetails.Count, actual: savedOrder.OrderDetails?.Count);

        _emailServiceMock.Verify(ws => ws
            .SendEmailAsync(order.User.Email, "Test", "testing"), Times.Once);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetOrdersByUserAsync_ShouldReturnUserOrders()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var orders = new List<Order>
        {
            new() {Status = OrderStatus.Pending, UserId = userId, OrderDetails = [] },
            new() {Status = OrderStatus.Processing, UserId = userId, OrderDetails = [] }
        };
        _context.Orders.AddRange(orders);
        await _context.SaveChangesAsync();

        // Act
        var userOrders = await _orderService.GetOrdersByUserAsync(userId);

        // Assert
        Assert.Equal(2, userOrders.Count());
    }
}
