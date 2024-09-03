using System;
using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface ICartItemService
{
    Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId);
    Task AddItemAsync(CartItem item);
    Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId);
    Task<CartItem> GetCartItemByIdAsync(int cartItemId);
}
