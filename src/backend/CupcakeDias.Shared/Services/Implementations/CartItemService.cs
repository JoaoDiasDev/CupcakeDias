using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Shared.Services.Implementations;

public class CartItemService(CupcakeDiasContext context) : ICartItemService
{
    public async Task AddItemAsync(CartItem item)
    {
        context.CartItems.Add(item);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(Guid userId)
    {
        return await context
            .Carts
            .Where(c => c.UserId == userId).SelectMany(c => c.CartItems ?? new List<CartItem>()).ToListAsync();
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(Guid cartId)
    {
        return await context.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();
    }

    public async Task<CartItem> GetCartItemByIdAsync(Guid cartItemId)
    {
        return await context.CartItems.FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId) ?? new CartItem();
    }

    public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
    {
        context.CartItems.Update(cartItem);
        await context.SaveChangesAsync();
        return cartItem;
    }
}