using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        var createdOrder = await orderService.CreateOrderAsync(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderID }, createdOrder);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await orderService.GetOrdersByUserAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }
}
