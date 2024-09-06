using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Consts;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetOrdersByUserAsync(Guid userId);
    Task<Order> CreateOrderAsync(Order order);
    Task SendOrderConfirmationEmailAsync(Order order);
    Task<Order> UpdateOrderStatusAsync(Order order, OrderStatus status);
    Task<Order> GetOrderByIdAsync(Guid orderId);
}
