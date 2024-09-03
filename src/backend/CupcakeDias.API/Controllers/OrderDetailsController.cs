using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailsController(IOrderDetailService orderDetailService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrderDetail([FromBody] OrderDetail orderDetail)
    {
        if (orderDetail == null)
        {
            return BadRequest("Order detail is null.");
        }

        var createdOrderDetail = await orderDetailService.CreateOrderDetailAsync(orderDetail);
        return CreatedAtAction(nameof(GetOrderDetailById), new { id = createdOrderDetail.OrderDetailId }, createdOrderDetail);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderDetailById(int id)
    {
        var orderDetail = await orderDetailService.GetOrderDetailByIdAsync(id);
        if (orderDetail == null)
        {
            return NotFound();
        }
        return Ok(orderDetail);
    }

    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetOrderDetailsByOrderId(int orderId)
    {
        var orderDetails = await orderDetailService.GetOrderDetailsByOrderIdAsync(orderId);
        if (orderDetails == null || !orderDetails.Any())
        {
            return NotFound();
        }
        return Ok(orderDetails);
    }
}
