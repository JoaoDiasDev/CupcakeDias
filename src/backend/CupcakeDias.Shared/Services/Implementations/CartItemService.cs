using System;
using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Shared.Services.Implementations;

public class CartItemService(CupcakeDiasContext context) : ICartItemService
{
    private readonly CupcakeDiasContext _context = context;

    public async Task AddItemAsync(CartItem item)
    {
        _context.CartItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId)
    {
        return await _context.Carts.Where(c => c.UserId == userId).SelectMany(c => c.CartItems).ToListAsync();
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId)
    {
        return await _context.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();
    }

    public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
    {
        return await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);
    }
}