using System;
using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;

namespace CupcakeDias.Shared.Services.Implementations;

public class OrderService(CupcakeDiasContext cupcakeDiasContext, IEmailService emailService) : IOrderService
{
    public async Task<Order> CreateOrderAsync(Order order)
    {
        cupcakeDiasContext.Orders.Add(order);
        await cupcakeDiasContext.SaveChangesAsync();
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserAsync(Guid userId)
    {
        return await cupcakeDiasContext.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Cupcake)
            .ToListAsync();
    }

    public async Task SendOrderConfirmationEmailAsync(Order order)
    {
        if (order is null || Guid.Empty.Equals(order.UserId) || order?.OrderDetails?.Count == 0) return;

        string subject = $"Your order {order?.OrderId} is being processed!";
        string message = $"Dear {order?.User?.Name},\nYour order with ID {order?.OrderId} is now being processed. Your order will be delivered soon.";

        var orderTotal = default(decimal);
        foreach (var orderDetail in order?.OrderDetails ?? [])
        {
            var cupcakeTotal = (orderDetail?.Price * orderDetail?.Quantity) ?? 0M;
            orderTotal += cupcakeTotal;
            message += $"\n{orderDetail?.Cupcake?.Name}";
            message += $"\n{orderDetail?.Price} x {orderDetail?.Quantity} = {cupcakeTotal}";
        }
        message += "\nTotal: " + orderTotal;
        message += "\nThank you for using our service!";
        message += "\nCupcakeDias Team - JoaoDiasDev";

        await emailService.SendEmailAsync(order?.User?.Email ?? string.Empty, subject, message);
    }

}
