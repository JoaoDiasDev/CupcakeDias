using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface IOrderDetailService
{
    Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail);
    Task<OrderDetail> GetOrderDetailByIdAsync(int orderDetailId);
    Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId);
}
