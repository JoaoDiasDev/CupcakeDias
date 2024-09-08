using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface ICupcakeService
{
    Task<Cupcake> CreateCupcakeAsync(Cupcake cupcake, List<Guid> ingredientIds);
    Task<Cupcake> GetCupcakeByIdAsync(Guid cupcakeId);
    Task<IEnumerable<Cupcake>> GetAllCupcakesAsync();
    Task UpdateCupcakeAsync(Cupcake cupcake, List<Guid> ingredientIds);
    Task DeleteCupcakeAsync(Guid cupcakeId);
    Task AddIngredientsToCupcakeAsync(Guid cupcakeId, List<Guid> ingredientIds);
    Task RemoveIngredientFromCupcakeAsync(Guid cupcakeId, Guid ingredientId);
}
