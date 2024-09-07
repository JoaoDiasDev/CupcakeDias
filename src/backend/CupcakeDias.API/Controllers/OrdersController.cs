using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Consts;
using CupcakeDias.Shared.Dtos;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrdersController(ICartService cartService,
    IOrderService orderService,
    IOrderDetailService orderDetailService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var order = await orderService.GetAllOrdersAsync();
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetOrderByUserId(Guid userId)
    {
        var order = await orderService.GetOrdersByUserAsync(userId);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPut("{orderId:guid}/status")]
    public async Task<IActionResult> UpdateOrderStatus(Guid orderId,
        [FromBody] UpdateOrderStatusDto updateOrderStatusDto)
    {
        var order = await orderService.GetOrderByIdToUpdateStatusAsync(orderId);

        if (order == null) return NotFound();

        if (OrderStatus.Cancelled != updateOrderStatusDto.Status
            && OrderStatus.Pending != updateOrderStatusDto.Status
            && OrderStatus.Completed != updateOrderStatusDto.Status
            && OrderStatus.Processing != updateOrderStatusDto.Status) return BadRequest();

        var updatedOrder = await orderService.UpdateOrderStatusAsync(order, updateOrderStatusDto.Status);

        if (updatedOrder is null) return BadRequest();

        return Ok(updatedOrder);
    }


    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] Cart cart)
    {
        var cartUpdate = await cartService.GetCartByIdAsync(cart.CartId);
        // Update cart status to 'Completed'
        await cartService.UpdateCartStatusAsync(cartUpdate, CartStatus.Completed);

        // Create an order from the cart
        var order = new Order
        {
            UserId = cart.UserId,
            OrderDate = DateTime.UtcNow,
            Total = cart.CartItems?.Sum(ci => ci.Price * ci.Quantity) ?? 0,
            Status = OrderStatus.Pending,
        };

        var createdOrder = await orderService.CreateOrderAsync(order);
        // Create order details for each item in the cart
        foreach (var cartItem in cart.CartItems ?? [])
        {
            var orderDetail = new OrderDetail
            {
                OrderId = createdOrder.OrderId,
                CupcakeId = cartItem.CupcakeId,
                Quantity = cartItem.Quantity,
                Price = cartItem.Price,
            };

            await orderDetailService.CreateOrderDetailAsync(orderDetail);
        }

        // Send confirmation email
        await orderService.SendOrderConfirmationEmailAsync(createdOrder, cart.CartItems?.ToList() ?? []);

        return CreatedAtAction(nameof(GetOrderByUserId), new { userId = createdOrder.UserId }, createdOrder);
    }
}
