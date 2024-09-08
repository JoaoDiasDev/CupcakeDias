using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
                .AsNoTracking()
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderDetails)!
            .ThenInclude(od => od.Cupcake)
            .ToListAsync();
    }

    public async Task SendOrderConfirmationEmailAsync(Order order, List<CartItem> cartItems)
    {
        if (order is null || Guid.Empty.Equals(order.UserId) || order.OrderDetails?.Count == 0) return;

        var cultureInfo = new CultureInfo("pt-BR");

        var subject = $"Seu pedido {order.OrderId} está sendo processado!";
        var message = $"Caro(a) {order.User?.Name},\nSeu pedido com o ID {order.OrderId} está sendo processado. Seu pedido será entregue em breve.\n";

        foreach (var orderDetail in order?.OrderDetails ?? [])
        {
            var cupcake = cartItems.Find(c => c.Cupcake!.CupcakeId.Equals(orderDetail.CupcakeId));
            var cupcakeTotal = orderDetail.Price * orderDetail.Quantity;

            var priceFormatted = orderDetail.Price.ToString("C", cultureInfo);
            var cupcakeTotalFormatted = cupcakeTotal.ToString("C", cultureInfo);

            message += $"\nCupcake - {cupcake?.Cupcake?.Name}";
            message += $"\n{priceFormatted} x {orderDetail?.Quantity} = {cupcakeTotalFormatted}\n";
        }

        var totalFormatted = order?.Total.ToString("C", cultureInfo);
        message += "\n\nTotal: " + totalFormatted;
        message += "\n\nObrigado por utilizar nossos serviços!";
        message += "\nEquipe CupcakeDias - JoaoDiasDev";

        await emailService.SendEmailAsync(order?.User?.Email ?? string.Empty, subject, message);
    }

    public async Task<Order> UpdateOrderStatusAsync(Order order, string status)
    {
        order.Status = status;
        cupcakeDiasContext.Orders.Update(order);
        await cupcakeDiasContext.SaveChangesAsync();
        return order;
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        return (await cupcakeDiasContext.Orders
                .AsNoTracking()
            .Where(o => o.OrderId == orderId)
            .Include(o => o.OrderDetails)!
            .ThenInclude(od => od.Cupcake)
            .FirstOrDefaultAsync())!;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await cupcakeDiasContext.Orders
            .AsNoTracking()
            .Include(o => o.OrderDetails)!
            .ThenInclude(od => od.Cupcake)
            .ToListAsync();
    }

    public async Task<Order> GetOrderByIdToUpdateStatusAsync(Guid orderId)
    {
        return (await cupcakeDiasContext.Orders
              .AsNoTracking()
              .Where(o => o.OrderId == orderId)
              .FirstOrDefaultAsync())!;
    }

}
