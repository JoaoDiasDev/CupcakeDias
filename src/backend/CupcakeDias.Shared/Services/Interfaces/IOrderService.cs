using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<IEnumerable<Order>> GetOrdersByUserAsync(Guid userId);
    Task<Order> CreateOrderAsync(Order order);
    Task SendOrderConfirmationEmailAsync(Order order, List<CartItem> cartItems);
    Task<Order> UpdateOrderStatusAsync(Order order, string status);
    Task<Order> GetOrderByIdAsync(Guid orderId);
    Task<Order> GetOrderByIdToUpdateStatusAsync(Guid orderId);
}
