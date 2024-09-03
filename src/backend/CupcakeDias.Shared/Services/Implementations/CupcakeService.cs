using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CupcakeDias.Shared.Services.Implementations;

public class CupcakeService(CupcakeDiasContext context) : ICupcakeService
{
    private readonly CupcakeDiasContext _context = context;

    public async Task<Cupcake> CreateCupcakeAsync(Cupcake cupcake)
    {
        _context.Cupcakes.Add(cupcake);
        await _context.SaveChangesAsync();
        return cupcake;
    }

    public async Task<Cupcake> GetCupcakeByIdAsync(int cupcakeId)
    {
        return await _context.Cupcakes
                             .Include(c => c.OrderDetails)
                             .Include(c => c.CupcakeIngredients)
                             .Include(c => c.CartItems)
                             .FirstOrDefaultAsync(c => c.CupcakeId == cupcakeId);
    }

    public async Task<IEnumerable<Cupcake>> GetAllCupcakesAsync()
    {
        return await _context.Cupcakes
                             .Include(c => c.OrderDetails)
                             .Include(c => c.CupcakeIngredients)
                             .Include(c => c.CartItems)
                             .ToListAsync();
    }

    public async Task UpdateCupcakeAsync(Cupcake cupcake)
    {
        _context.Cupcakes.Update(cupcake);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCupcakeAsync(int cupcakeId)
    {
        var cupcake = await _context.Cupcakes.FindAsync(cupcakeId);
        if (cupcake is not null)
        {
            _context.Cupcakes.Remove(cupcake);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddIngredientsToCupcakeAsync(int cupcakeId, List<int> ingredientIds)
    {
        var cupcake = await _context.Cupcakes.FindAsync(cupcakeId) ?? throw new KeyNotFoundException("Cupcake not found");

        foreach (var ingredientId in ingredientIds)
        {
            if (await _context.Ingredients.FindAsync(ingredientId) != null)
            {
                var cupcakeIngredient = new CupcakeIngredient
                {
                    CupcakeId = cupcakeId,
                    IngredientId = ingredientId
                };
                _context.CupcakeIngredients.Add(cupcakeIngredient);
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemoveIngredientFromCupcakeAsync(int cupcakeId, int ingredientId)
    {
        var cupcakeIngredient = await _context.CupcakeIngredients
            .FirstOrDefaultAsync(ci => ci.CupcakeId == cupcakeId && ci.IngredientId == ingredientId);

        if (cupcakeIngredient is not null)
        {
            _context.CupcakeIngredients.Remove(cupcakeIngredient);
            await _context.SaveChangesAsync();
        }
    }
}
