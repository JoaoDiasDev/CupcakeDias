using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartItemsController(ICartItemService cartItemService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddCartItem([FromBody] CartItem cartItem)
    {
        await cartItemService.AddItemAsync(cartItem);
        return CreatedAtAction(nameof(GetCartItemById), new { id = cartItem.CartItemId }, cartItem);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetCartItemsByUser(int userId)
    {
        var cartItems = await cartItemService.GetCartItemsByUserIdAsync(userId);
        if (!cartItems.Any())
        {
            return NotFound();
        }
        return Ok(cartItems);
    }

    [HttpGet("cart/{cartId:int}")]
    public async Task<IActionResult> GetCartItemsByCartId(int cartId)
    {
        var cartItems = await cartItemService.GetCartItemsByCartIdAsync(cartId);
        if (!cartItems.Any())
        {
            return NotFound();
        }
        return Ok(cartItems);
    }

    [HttpGet("{cartItemId:int}")]
    public async Task<IActionResult> GetCartItemById(int cartItemId)
    {
        var cartItem = await cartItemService.GetCartItemByIdAsync(cartItemId);
        if (cartItem == null)
        {
            return NotFound();
        }
        return Ok(cartItem);
    }
}

