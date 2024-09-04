using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Test.Services;

public class IngredientServiceTests
{
    private readonly IngredientService _ingredientService;
    private readonly CupcakeDiasContext _context;

    public IngredientServiceTests()
    {
        var options = new DbContextOptionsBuilder<CupcakeDiasContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new CupcakeDiasContext(options);
        _ingredientService = new IngredientService(_context);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task CreateIngredientAsync_ShouldAddIngredientToDatabase()
    {
        // Arrange
        var ingredient = new Ingredient { Name = "Vanilla Extract", Type = "Flavoring", Availability = true };

        // Act
        var createdIngredient = await _ingredientService.CreateIngredientAsync(ingredient);

        // Assert
        var savedIngredient = await _context.Ingredients.FirstOrDefaultAsync();
        Assert.NotNull(savedIngredient);
        Assert.Equal(ingredient.Name, savedIngredient.Name);
        Assert.Equal(ingredient.Type, savedIngredient.Type);
        Assert.Equal(ingredient.Availability, savedIngredient.Availability);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetIngredientByIdAsync_ShouldReturnIngredientWithDetails()
    {
        // Arrange
        var ingredient = new Ingredient
        {
            Name = "Cocoa Powder",
            Type = "Flavoring",
            Availability = true,
            CupcakeIngredients = new List<CupcakeIngredient>
            {
                new() { CupcakeId = Guid.NewGuid() }
            }
        };
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

        // Act
        var retrievedIngredient = await _ingredientService.GetIngredientByIdAsync(ingredient.IngredientId);

        // Assert
        Assert.NotNull(retrievedIngredient);
        Assert.Single(retrievedIngredient.CupcakeIngredients!);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetAllIngredientsAsync_ShouldReturnAllIngredients()
    {
        // Arrange
        var ingredients = new List<Ingredient>
        {
            new() { Name = "Sugar", Type = "Sweetener", Availability = true },
            new() { Name = "Butter", Type = "Fat", Availability = true }
        };
        _context.Ingredients.AddRange(ingredients);
        await _context.SaveChangesAsync();

        // Act
        var allIngredients = await _ingredientService.GetAllIngredientsAsync();

        // Assert
        Assert.Equal(2, allIngredients.Count());
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task UpdateIngredientAsync_ShouldUpdateIngredientInDatabase()
    {
        // Arrange
        var ingredient = new Ingredient { Name = "Milk", Type = "Liquid", Availability = true };
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

        // Act
        ingredient.Availability = false;
        await _ingredientService.UpdateIngredientAsync(ingredient);

        // Assert
        var updatedIngredient = await _context.Ingredients
            .FirstOrDefaultAsync(i => i.IngredientId == ingredient.IngredientId);

        Assert.NotNull(updatedIngredient);
        Assert.False(updatedIngredient.Availability);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task DeleteIngredientAsync_ShouldRemoveIngredientFromDatabase()
    {
        // Arrange
        var ingredient = new Ingredient { Name = "Salt", Type = "Seasoning", Availability = true };
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

        // Act
        await _ingredientService.DeleteIngredientAsync(ingredient.IngredientId);

        // Assert
        var deletedIngredient = await _context.Ingredients.FindAsync(ingredient.IngredientId);
        Assert.Null(deletedIngredient);
    }
}
