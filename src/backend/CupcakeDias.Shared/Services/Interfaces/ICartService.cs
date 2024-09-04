using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface ICartService
{
    Task<Cart> CreateCartAsync(Cart cart);
    Task<Cart> GetCartByIdAsync(Guid cartId);
    Task<IEnumerable<Cart>> GetCartsByUserIdAsync(Guid userId);
    Task UpdateCartAsync(Cart cart);
    Task DeleteCartAsync(Guid cartId);
}
