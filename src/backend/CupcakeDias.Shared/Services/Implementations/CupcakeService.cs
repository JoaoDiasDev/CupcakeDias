using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Shared.Services.Implementations;

public class CupcakeService(CupcakeDiasContext context) : ICupcakeService
{
    public async Task<Cupcake> CreateCupcakeAsync(Cupcake cupcake, List<Guid> ingredientIds)
    {
        context.Cupcakes.Add(cupcake);
        foreach (var ingredientId in ingredientIds)
        {
            if (await context.Ingredients.FindAsync(ingredientId) != null)
            {
                var cupcakeIngredient = new CupcakeIngredient
                {
                    CupcakeId = cupcake.CupcakeId,
                    IngredientId = ingredientId
                };
                context.CupcakeIngredients.Add(cupcakeIngredient);
            }
        }
        await context.SaveChangesAsync();
        return cupcake;
    }

    public async Task<Cupcake> GetCupcakeByIdAsync(Guid cupcakeId)
    {
        return await context.Cupcakes
            .AsNoTracking()
            .Include(c => c.OrderDetails)
            .Include(c => c.CupcakeIngredients!)
            .ThenInclude(ci => ci.Ingredient)
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.CupcakeId == cupcakeId) ?? new Cupcake { BaseFlavor = "Doe", Name = "John", ImageUrl = "https://jondoen.com" };
    }

    public async Task<IEnumerable<Cupcake>> GetAllCupcakesAsync()
    {
        return await context.Cupcakes
                .AsNoTracking()
                             .Include(c => c.OrderDetails)
                             .Include(c => c.CupcakeIngredients!)
                             .ThenInclude(ci => ci.Ingredient)
                             .Include(c => c.CartItems)
                             .ToListAsync();
    }
    public async Task UpdateCupcakeAsync(Cupcake cupcake, List<Guid> ingredientIds)
    {
        // Find the already tracked entity (if any) and modify it
        var existingCupcake = await context.Cupcakes
            .Include(c => c.CupcakeIngredients)  // Load related data (ingredients)
            .FirstOrDefaultAsync(c => c.CupcakeId == cupcake.CupcakeId);

        if (existingCupcake == null)
        {
            throw new KeyNotFoundException("Cupcake not found");
        }

        // Remove existing ingredients
        context.CupcakeIngredients.RemoveRange(existingCupcake.CupcakeIngredients!);

        // Add new ingredients
        foreach (var ingredientId in ingredientIds)
        {
            var ingredient = await context.Ingredients.FindAsync(ingredientId);
            if (ingredient != null)
            {
                var cupcakeIngredient = new CupcakeIngredient
                {
                    CupcakeId = cupcake.CupcakeId,
                    IngredientId = ingredientId
                };
                context.CupcakeIngredients.Add(cupcakeIngredient);
            }
        }

        // Update the existing entity's properties (no need to attach)
        existingCupcake.Name = cupcake.Name;
        existingCupcake.Price = cupcake.Price;
        existingCupcake.Description = cupcake.Description;
        existingCupcake.ImageUrl = cupcake.ImageUrl;
        existingCupcake.BaseFlavor = cupcake.BaseFlavor;

        // Save changes to the database
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
        var cupcake = await context.Cupcakes.FirstOrDefaultAsync(c => c.CupcakeId.Equals(cupcakeId));
        if (cupcake is null) throw new ArgumentNullException();

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
