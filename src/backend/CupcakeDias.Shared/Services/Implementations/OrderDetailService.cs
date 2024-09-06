using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Shared.Services.Implementations;

public class OrderDetailService(CupcakeDiasContext context) : IOrderDetailService
{
    public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail)
    {
        context.OrderDetails.Add(orderDetail);
        await context.SaveChangesAsync();
        return orderDetail;
    }

    public async Task<OrderDetail> GetOrderDetailByIdAsync(Guid orderDetailId)
    {
        return await context.OrderDetails
                .AsNoTracking()
            .Include(od => od.Order)
            .Include(od => od.Cupcake)
            .FirstOrDefaultAsync(od => od.OrderDetailId.Equals(orderDetailId)) ?? new OrderDetail();
    }

    public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(Guid orderId)
    {
        return await context.OrderDetails
                .AsNoTracking()
                             .Include(od => od.Order)
                             .Include(od => od.Cupcake)
                             .Where(od => od.OrderId == orderId)
                             .ToListAsync();
    }
}