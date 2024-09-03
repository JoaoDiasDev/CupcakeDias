using CupcakeDias.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface ICartService
{
    Task<Cart> CreateCartAsync(Cart cart);
    Task<Cart> GetCartByIdAsync(int cartId);
    Task<IEnumerable<Cart>> GetCartsByUserIdAsync(int userId);
    Task UpdateCartAsync(Cart cart);
    Task DeleteCartAsync(int cartId);
}
