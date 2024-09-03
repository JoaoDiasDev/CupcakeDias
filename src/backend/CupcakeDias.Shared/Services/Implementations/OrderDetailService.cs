using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CupcakeDias.Shared.Services.Implementations;

public class OrderDetailService(CupcakeDiasContext context) : IOrderDetailService
{
    private readonly CupcakeDiasContext _context = context;

    public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail)
    {
        _context.OrderDetails.Add(orderDetail);
        await _context.SaveChangesAsync();
        return orderDetail;
    }

    public async Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId)
    {
        return await _context.OrderDetails
                             .Include(od => od.Order)
                             .Include(od => od.Cupcake)
                             .FirstOrDefaultAsync(od => od.OrderDetailId == orderDetailId);
    }

    public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
    {
        return await _context.OrderDetails
                             .Include(od => od.Order)
                             .Include(od => od.Cupcake)
                             .Where(od => od.OrderId == orderId)
                             .ToListAsync();
    }
}