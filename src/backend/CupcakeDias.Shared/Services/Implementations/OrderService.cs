using System;
using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Dtos;
using CupcakeDias.Shared.Services.Interfaces;

namespace CupcakeDias.Shared.Services.Implementations;

public class OrderService(CupcakeDiasContext cupcakeDiasContext, IWhatsAppService whatsAppService) : IOrderService
{
    public async Task<Order> CreateOrderAsync(Order order)
    {
        cupcakeDiasContext.Orders.Add(order);
        await cupcakeDiasContext.SaveChangesAsync();

        if (order?.User?.PhoneNumber is null || order.User.PhoneNumber.Length == 0) return order!;

        // Send WhatsApp notification
        var message = $"Thank you for your order! Your order ID is {order.OrderId}.";

        var whatsAppMessageDto = new WhatsAppMessageDto
        {
            Message = message,
            PhoneNumber = order.User.PhoneNumber
        };

        await whatsAppService.SendMessageAsync(whatsAppMessageDto);

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId)
    {
        return await cupcakeDiasContext.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Cupcake)
            .ToListAsync();
    }
}
