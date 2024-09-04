using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CartItemsController(ICartItemService cartItemService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddCartItem([FromBody] CartItem cartItem)
    {
        await cartItemService.AddItemAsync(cartItem);
        return CreatedAtAction(nameof(GetCartItemById), new { id = cartItem.CartItemId }, cartItem);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetCartItemsByUser(Guid userId)
    {
        var cartItems = await cartItemService.GetCartItemsByUserIdAsync(userId);
        if (!cartItems.Any())
        {
            return NotFound();
        }
        return Ok(cartItems);
    }

    [HttpGet("cart/{cartId:guid}")]
    public async Task<IActionResult> GetCartItemsByCartId(Guid cartId)
    {
        var cartItems = await cartItemService.GetCartItemsByCartIdAsync(cartId);
        if (!cartItems.Any())
        {
            return NotFound();
        }
        return Ok(cartItems);
    }

    [HttpGet("{cartItemId:guid}")]
    public async Task<IActionResult> GetCartItemById(Guid cartItemId)
    {
        var cartItem = await cartItemService.GetCartItemByIdAsync(cartItemId);
        if (cartItem == null)
        {
            return NotFound();
        }
        return Ok(cartItem);
    }
}

