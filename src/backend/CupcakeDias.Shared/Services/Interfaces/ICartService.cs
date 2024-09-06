using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Consts;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface ICartService
{
    Task<Cart> CreateCartAsync(Cart cart);
    Task<Cart> GetCartByIdAsync(Guid cartId);
    Task<IEnumerable<Cart>> GetCartsByUserIdAsync(Guid userId);
    Task UpdateCartAsync(Cart cart);
    Task UpdateCartStatusAsync(Cart cart, CartStatus status);
    Task DeleteCartAsync(Guid cartId);
}
