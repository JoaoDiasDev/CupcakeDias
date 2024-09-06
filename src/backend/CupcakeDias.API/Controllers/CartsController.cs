using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Consts;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Authorize]
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

    [HttpGet("{cartId:guid}")]
    public async Task<IActionResult> GetCartById(Guid cartId)
    {
        var cart = await cartService.GetCartByIdAsync(cartId);
        if (cart == null)
        {
            return NotFound();
        }
        return Ok(cart);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetCartsByUserId(Guid userId)
    {
        var carts = await cartService.GetCartsByUserIdAsync(userId);
        var cartsList = carts.ToList();
        if (cartsList is null || !cartsList.Any())
        {
            var cart = new Cart
            {
                UserId = userId,
                Status = CartStatus.Open,
            };

            var cartCreated = await cartService.CreateCartAsync(cart);
            if (cartCreated is null) return BadRequest();

            cartsList?.Add(cartCreated);

        }

        var cartResponse = cartsList?.OrderByDescending(c => c.CreatedAt).FirstOrDefault(c =>
            !c.Status.Equals(CartStatus.Canceled)
            && !c.Status.Equals(CartStatus.Completed)) ?? new Cart { Status = CartStatus.Canceled };


        return Ok(cartResponse);
    }

    [HttpPut("{cartId:guid}")]
    public async Task<IActionResult> UpdateCart(Guid cartId, [FromBody] Cart cart)
    {
        if (cart is null || !(cart.CartId.Equals(cartId)))
        {
            return BadRequest("Cart is null or ID mismatch.");
        }

        var existingCart = await cartService.GetCartByIdAsync(cartId);
        if (existingCart == null)
        {
            return NotFound();
        }

        await cartService.UpdateCartAsync(cart);
        return NoContent();
    }

    [HttpDelete("{cartId:guid}")]
    public async Task<IActionResult> DeleteCart(Guid cartId)
    {
        var cart = await cartService.GetCartByIdAsync(cartId);
        if (cart == null)
        {
            return NotFound();
        }

        await cartService.DeleteCartAsync(cartId);
        return NoContent();
    }


    [HttpPut("{cartId:guid}/status")]
    public async Task<IActionResult> UpdateCart(Guid cartId, [FromBody] CartStatus status)
    {
        if (Guid.Empty.Equals(cartId)) return BadRequest("Cart is null or ID mismatch.");

        var existingCart = await cartService.GetCartByIdAsync(cartId);
        if (existingCart == null)
        {
            return NotFound($"No cart found with id {cartId}");
        }

        await cartService.UpdateCartStatusAsync(existingCart, status);
        return NoContent();
    }
}
