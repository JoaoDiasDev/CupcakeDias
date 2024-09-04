using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface IIngredientService
{
    Task<Ingredient> CreateIngredientAsync(Ingredient ingredient);
    Task<Ingredient> GetIngredientByIdAsync(Guid ingredientId);
    Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
    Task UpdateIngredientAsync(Ingredient ingredient);
    Task DeleteIngredientAsync(Guid ingredientId);
}
