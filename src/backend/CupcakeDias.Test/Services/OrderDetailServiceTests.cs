using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Consts;
using CupcakeDias.Shared.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Test.Services;

public class OrderDetailServiceTests
{
    private readonly OrderDetailService _orderDetailService;
    private readonly CupcakeDiasContext _context;

    public OrderDetailServiceTests()
    {
        var options = new DbContextOptionsBuilder<CupcakeDiasContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new CupcakeDiasContext(options);
        _orderDetailService = new OrderDetailService(_context);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task CreateOrderDetailAsync_ShouldAddOrderDetailToDatabase()
    {
        // Arrange
        var orderDetail = new OrderDetail
        {
            OrderId = Guid.NewGuid(),
            CupcakeId = Guid.NewGuid(),
            Quantity = 2,
            Price = 5.00m
        };

        // Act
        var createdOrderDetail = await _orderDetailService.CreateOrderDetailAsync(orderDetail);

        // Assert
        var savedOrderDetail = await _context.OrderDetails.FirstOrDefaultAsync();
        Assert.NotNull(savedOrderDetail);
        Assert.Equal(orderDetail.OrderId, savedOrderDetail.OrderId);
        Assert.Equal(orderDetail.CupcakeId, savedOrderDetail.CupcakeId);
        Assert.Equal(orderDetail.Quantity, savedOrderDetail.Quantity);
        Assert.Equal(orderDetail.Price, savedOrderDetail.Price);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetOrderDetailByIdAsync_ShouldReturnOrderDetailWithDetails()
    {
        // Arrange
        var order = new Order { Total = 0, Status = OrderStatus.Pending, UserId = Guid.NewGuid() };
        var cupcake = new Cupcake
        {
            Name = "Chocolate",
            BaseFlavor = "Chocolate",
            Price = 4.00m,
            ImageUrl = "https://jaodias.com"
        };
        var orderDetail = new OrderDetail { Order = order, Cupcake = cupcake, Quantity = 2, Price = 4.00m };
        _context.Orders.Add(order);
        _context.Cupcakes.Add(cupcake);
        _context.OrderDetails.Add(orderDetail);
        await _context.SaveChangesAsync();

        // Act
        var retrievedOrderDetail = await _orderDetailService.GetOrderDetailByIdAsync(orderDetail.OrderDetailId);

        // Assert
        Assert.NotNull(retrievedOrderDetail);
        Assert.Equal(orderDetail.OrderDetailId, retrievedOrderDetail.OrderDetailId);
        Assert.Equal(orderDetail.OrderId, retrievedOrderDetail.OrderId);
        Assert.Equal(orderDetail.CupcakeId, retrievedOrderDetail.CupcakeId);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetOrderDetailsByOrderIdAsync_ShouldReturnAllOrderDetailsForOrder()
    {
        // Arrange
        var order = new Order { Status = OrderStatus.Pending, Total = 0, UserId = Guid.NewGuid() };
        var orderDetails = new List<OrderDetail>
        {
            new() { Order = order, CupcakeId = Guid.NewGuid(), Quantity = 2, Price = 4.00m },
            new() { Order = order, CupcakeId = Guid.NewGuid(), Quantity = 1, Price = 7.50m }
        };
        _context.Orders.Add(order);
        _context.OrderDetails.AddRange(orderDetails);
        await _context.SaveChangesAsync();

        // Act
        var retrievedOrderDetails = await _orderDetailService
            .GetOrderDetailsByOrderIdAsync(order.OrderId);

        // Assert
        var enumerable = retrievedOrderDetails.ToList();
        Assert.Equal(2, enumerable.Count());
        Assert.Equal(4.00m, enumerable.First().Price);
        Assert.Equal(7.50m, enumerable.Last().Price);
    }
}
