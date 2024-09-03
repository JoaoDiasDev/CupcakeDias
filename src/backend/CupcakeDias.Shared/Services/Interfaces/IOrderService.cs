using System;
using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId);
    Task<Order> CreateOrderAsync(Order order);
}
