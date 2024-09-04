using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Shared.Services.Implementations;

public class IngredientService(CupcakeDiasContext context) : IIngredientService
{
    public async Task<Ingredient> CreateIngredientAsync(Ingredient ingredient)
    {
        context.Ingredients.Add(ingredient);
        await context.SaveChangesAsync();
        return ingredient;
    }

    public async Task<Ingredient> GetIngredientByIdAsync(Guid ingredientId)
    {
        return await context.Ingredients
            .Include(i => i.CupcakeIngredients)
            .FirstOrDefaultAsync(i => i.IngredientId == ingredientId) ?? new Ingredient { Name = "", Availability = true, Type = "" };
    }

    public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
    {
        return await context.Ingredients
                             .Include(i => i.CupcakeIngredients)
                             .ToListAsync();
    }

    public async Task UpdateIngredientAsync(Ingredient ingredient)
    {
        context.Ingredients.Update(ingredient);
        await context.SaveChangesAsync();
    }

    public async Task DeleteIngredientAsync(Guid ingredientId)
    {
        var ingredient = await context.Ingredients.FindAsync(ingredientId);
        if (ingredient != null)
        {
            context.Ingredients.Remove(ingredient);
            await context.SaveChangesAsync();
        }
    }
}
