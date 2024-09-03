using CupcakeDias.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CupcakeDias.Shared.Services.Interfaces;

public interface IIngredientService
{
    Task<Ingredient> CreateIngredientAsync(Ingredient ingredient);
    Task<Ingredient> GetIngredientByIdAsync(int ingredientId);
    Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
    Task UpdateIngredientAsync(Ingredient ingredient);
    Task DeleteIngredientAsync(int ingredientId);
}
