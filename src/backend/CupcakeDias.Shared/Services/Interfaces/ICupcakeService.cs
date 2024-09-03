using CupcakeDias.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface ICupcakeService
{
    Task<Cupcake> CreateCupcakeAsync(Cupcake cupcake);
    Task<Cupcake> GetCupcakeByIdAsync(int cupcakeId);
    Task<IEnumerable<Cupcake>> GetAllCupcakesAsync();
    Task UpdateCupcakeAsync(Cupcake cupcake);
    Task DeleteCupcakeAsync(int cupcakeId);
    Task AddIngredientsToCupcakeAsync(int cupcakeId, List<int> ingredientIds);
    Task RemoveIngredientFromCupcakeAsync(int cupcakeId, int ingredientId);
}
