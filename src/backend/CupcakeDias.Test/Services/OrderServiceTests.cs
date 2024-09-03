using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Consts;
using CupcakeDias.Shared.Dtos;
using CupcakeDias.Shared.Services.Implementations;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CupcakeDias.Test.Services;

public class OrderServiceTests
{
    private readonly OrderService _orderService;
    private readonly Mock<IWhatsAppService> _mockWhatsAppService;
    private readonly CupcakeDiasContext _context;

    public OrderServiceTests()
    {
        var options = new DbContextOptionsBuilder<CupcakeDiasContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        _context = new CupcakeDiasContext(options);
        _mockWhatsAppService = new Mock<IWhatsAppService>();
        _orderService = new OrderService(_context, _mockWhatsAppService.Object);
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
                PhoneNumber = "+1234567890"
            },

            OrderDetails =
            [
                new() { CupcakeId = 1, Quantity = 2, Price = 5.00m }
            ]
        };

        // Act
        var createdOrder = await _orderService.CreateOrderAsync(order);

        // Assert
        var savedOrder = await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync();
        Assert.NotNull(savedOrder);
        Assert.Equal(order.OrderDetails.Count, actual: savedOrder?.OrderDetails?.Count);
        var whatsAppMessageDto = new WhatsAppMessageDto
        {
            Message = $"Thank you for your order! Your order ID is {createdOrder.OrderId}.",
            PhoneNumber = "+1234567890"
        };
        _mockWhatsAppService.Verify(ws => ws.SendMessageAsync(whatsAppMessageDto), Times.Once);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetOrdersByUserAsync_ShouldReturnUserOrders()
    {
        // Arrange
        var userId = 1;
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
