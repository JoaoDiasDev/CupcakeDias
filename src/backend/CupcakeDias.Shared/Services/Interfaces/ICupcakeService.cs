using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface ICupcakeService
{
    Task<Cupcake> CreateCupcakeAsync(Cupcake cupcake);
    Task<Cupcake> GetCupcakeByIdAsync(Guid cupcakeId);
    Task<IEnumerable<Cupcake>> GetAllCupcakesAsync();
    Task UpdateCupcakeAsync(Cupcake cupcake);
    Task DeleteCupcakeAsync(Guid cupcakeId);
    Task AddIngredientsToCupcakeAsync(Guid cupcakeId, List<Guid> ingredientIds);
    Task RemoveIngredientFromCupcakeAsync(Guid cupcakeId, Guid ingredientId);
}
