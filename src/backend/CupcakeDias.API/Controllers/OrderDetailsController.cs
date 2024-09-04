using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Authorize]
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
        return CreatedAtAction(nameof(GetOrderDetailById),
            new { orderDetailsId = createdOrderDetail.OrderDetailId }, createdOrderDetail);
    }

    [HttpGet("{orderDetailsId:guid}")]
    public async Task<IActionResult> GetOrderDetailById(Guid orderDetailsId)
    {
        var orderDetail = await orderDetailService.GetOrderDetailByIdAsync(orderDetailsId);
        if (orderDetail == null)
        {
            return NotFound();
        }
        return Ok(orderDetail);
    }

    [HttpGet("order/{orderId:guid}")]
    public async Task<IActionResult> GetOrderDetailsByOrderId(Guid orderId)
    {
        var orderDetails = await orderDetailService.GetOrderDetailsByOrderIdAsync(orderId);
        if (orderDetails == null || !orderDetails.Any())
        {
            return NotFound();
        }
        return Ok(orderDetails);
    }
}
