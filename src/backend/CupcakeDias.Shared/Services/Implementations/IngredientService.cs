using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CupcakeDias.Shared.Services.Implementations;

public class IngredientService(CupcakeDiasContext context) : IIngredientService
{
    private readonly CupcakeDiasContext _context = context;

    public async Task<Ingredient> CreateIngredientAsync(Ingredient ingredient)
    {
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();
        return ingredient;
    }

    public async Task<Ingredient> GetIngredientByIdAsync(int ingredientId)
    {
        return await _context.Ingredients
                             .Include(i => i.CupcakeIngredients)
                             .FirstOrDefaultAsync(i => i.IngredientId == ingredientId);
    }

    public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
    {
        return await _context.Ingredients
                             .Include(i => i.CupcakeIngredients)
                             .ToListAsync();
    }

    public async Task UpdateIngredientAsync(Ingredient ingredient)
    {
        _context.Ingredients.Update(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteIngredientAsync(int ingredientId)
    {
        var ingredient = await _context.Ingredients.FindAsync(ingredientId);
        if (ingredient != null)
        {
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }
    }
}
