using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController(ICartService cartService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCart([FromBody] Cart cart)
    {
        if (cart == null)
        {
            return BadRequest("Cart is null.");
        }

        var createdCart = await cartService.CreateCartAsync(cart);
        return CreatedAtAction(nameof(GetCartById), new { id = createdCart.CartId }, createdCart);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCartById(int id)
    {
        var cart = await cartService.GetCartByIdAsync(id);
        if (cart == null)
        {
            return NotFound();
        }
        return Ok(cart);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetCartsByUserId(int userId)
    {
        var carts = await cartService.GetCartsByUserIdAsync(userId);
        if (carts == null || !carts.Any())
        {
            return NotFound();
        }
        return Ok(carts);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCart(int id, [FromBody] Cart cart)
    {
        if (cart == null || cart.CartId != id)
        {
            return BadRequest("Cart is null or ID mismatch.");
        }

        var existingCart = await cartService.GetCartByIdAsync(id);
        if (existingCart == null)
        {
            return NotFound();
        }

        await cartService.UpdateCartAsync(cart);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(int id)
    {
        var cart = await cartService.GetCartByIdAsync(id);
        if (cart == null)
        {
            return NotFound();
        }

        await cartService.DeleteCartAsync(id);
        return NoContent();
    }
}
