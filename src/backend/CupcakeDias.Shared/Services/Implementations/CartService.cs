using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Consts;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Shared.Services.Implementations;

public class CartService(CupcakeDiasContext context) : ICartService
{
    public async Task<Cart> CreateCartAsync(Cart cart)
    {
        context.Carts.Add(cart);
        await context.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart> GetCartByIdAsync(Guid cartId)
    {
        return await context.Carts
                .AsNoTracking()
            .Include(c => c.User)
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.CartId == cartId) ?? new Cart { Status = CartStatus.Canceled };
    }

    public async Task<IEnumerable<Cart>> GetCartsByUserIdAsync(Guid userId)
    {
        return await context.Carts
                .AsNoTracking()
                             .Include(c => c.CartItems!)
                             .ThenInclude(ci => ci.Cupcake)
                             .Where(c => c.UserId == userId && c.Status.Equals(CartStatus.Open))
                             .ToListAsync();
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        context.Carts.Update(cart);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCartAsync(Guid cartId)
    {
        var cart = await context.Carts.FindAsync(cartId);
        if (cart is not null)
        {
            context.Carts.Remove(cart);
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateCartStatusAsync(Cart cart, string status)
    {
        cart.Status = status;
        context.Carts.Update(cart);
        await context.SaveChangesAsync();
    }
}
