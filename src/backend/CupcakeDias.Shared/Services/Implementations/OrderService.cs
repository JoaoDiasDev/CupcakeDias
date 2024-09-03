using System;
using CupcakeDias.Data;
using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Implementations;

public class OrderService(CupcakeDiasContext cupcakeDiasContext)
{
    public async Task<Order> CreateOrderAsync(Order order)
    {
        cupcakeDiasContext.Orders.Add(order);
        await cupcakeDiasContext.SaveChangesAsync();
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId)
    {
        return await cupcakeDiasContext.Orders
            .Where(o => o.UserID == userId)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Cupcake)
            .ToListAsync();
    }
}
