using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Shared.Services.Implementations;

public class CupcakeService(CupcakeDiasContext context) : ICupcakeService
{
    public async Task<Cupcake> CreateCupcakeAsync(Cupcake cupcake)
    {
        context.Cupcakes.Add(cupcake);
        await context.SaveChangesAsync();
        return cupcake;
    }

    public async Task<Cupcake> GetCupcakeByIdAsync(Guid cupcakeId)
    {
        return await context.Cupcakes
            .Include(c => c.OrderDetails)
            .Include(c => c.CupcakeIngredients)
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.CupcakeId == cupcakeId) ?? new Cupcake { BaseFlavor = "Doe", Name = "John", ImageUrl = "https://jondoen.com" };
    }

    public async Task<IEnumerable<Cupcake>> GetAllCupcakesAsync()
    {
        return await context.Cupcakes
                             .Include(c => c.OrderDetails)
                             .Include(c => c.CupcakeIngredients)
                             .Include(c => c.CartItems)
                             .ToListAsync();
    }

    public async Task UpdateCupcakeAsync(Cupcake cupcake)
    {
        context.Cupcakes.Update(cupcake);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCupcakeAsync(Guid cupcakeId)
    {
        var cupcake = await context.Cupcakes.FindAsync(cupcakeId);
        if (cupcake is not null)
        {
            context.Cupcakes.Remove(cupcake);
            await context.SaveChangesAsync();
        }
    }

    public async Task AddIngredientsToCupcakeAsync(Guid cupcakeId, List<Guid> ingredientIds)
    {
        var cupcake = await context.Cupcakes.FindAsync(cupcakeId) ?? throw new KeyNotFoundException("Cupcake not found");

        foreach (var ingredientId in ingredientIds)
        {
            if (await context.Ingredients.FindAsync(ingredientId) != null)
            {
                var cupcakeIngredient = new CupcakeIngredient
                {
                    CupcakeId = cupcakeId,
                    IngredientId = ingredientId
                };
                context.CupcakeIngredients.Add(cupcakeIngredient);
            }
        }

        await context.SaveChangesAsync();
    }

    public async Task RemoveIngredientFromCupcakeAsync(Guid cupcakeId, Guid ingredientId)
    {
        var cupcakeIngredient = await context.CupcakeIngredients
            .FirstOrDefaultAsync(ci => ci.CupcakeId == cupcakeId && ci.IngredientId == ingredientId);

        if (cupcakeIngredient is not null)
        {
            context.CupcakeIngredients.Remove(cupcakeIngredient);
            await context.SaveChangesAsync();
        }
    }
}
