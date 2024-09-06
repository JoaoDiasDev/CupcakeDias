using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface ICartItemService
{
    Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(Guid userId);
    Task AddItemAsync(CartItem item);
    Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(Guid cartId);
    Task<CartItem> GetCartItemByIdAsync(Guid cartItemId);
    Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
}
