using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CupcakeDias.Shared.Services.Implementations;

public class CartService(CupcakeDiasContext context) : ICartService
{
    private readonly CupcakeDiasContext _context = context;

    public async Task<Cart> CreateCartAsync(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart> GetCartByIdAsync(int cartId)
    {
        return await _context.Carts
                             .Include(c => c.User)
                             .Include(c => c.CartItems)
                             .FirstOrDefaultAsync(c => c.CartId == cartId);
    }

    public async Task<IEnumerable<Cart>> GetCartsByUserIdAsync(int userId)
    {
        return await _context.Carts
                             .Include(c => c.CartItems)
                             .Where(c => c.UserId == userId)
                             .ToListAsync();
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCartAsync(int cartId)
    {
        var cart = await _context.Carts.FindAsync(cartId);
        if (cart != null)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }
    }
}
