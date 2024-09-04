using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        var createdOrder = await orderService.CreateOrderAsync(order);

        await Task.Run(() => orderService.SendOrderConfirmationEmailAsync(createdOrder));

        return CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.OrderId }, createdOrder);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        var order = await orderService.GetOrdersByUserAsync(orderId);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }
}
