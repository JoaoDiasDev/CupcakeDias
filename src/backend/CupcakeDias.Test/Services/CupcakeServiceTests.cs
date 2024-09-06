using CupcakeDias.Data;
using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Test.Services;

public class CupcakeServiceTests
{
    private readonly CupcakeService _cupcakeService;
    private readonly CupcakeDiasContext _context;

    public CupcakeServiceTests()
    {
        var options = new DbContextOptionsBuilder<CupcakeDiasContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new CupcakeDiasContext(options);
        _cupcakeService = new CupcakeService(_context);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task CreateCupcakeAsync_ShouldAddCupcakeToDatabase()
    {
        // Arrange
        var cupcake = new Cupcake { Name = "Vanilla", BaseFlavor = "Vanilla", Price = 3.50m, ImageUrl = "https://jaoao.com" };

        // Act
        await _cupcakeService.CreateCupcakeAsync(cupcake, []);

        // Assert
        var savedCupcake = await _context.Cupcakes.FirstOrDefaultAsync();
        Assert.NotNull(savedCupcake);
        Assert.Equal(cupcake.Name, savedCupcake.Name);
        Assert.Equal(cupcake.BaseFlavor, savedCupcake.BaseFlavor);
        Assert.Equal(cupcake.Price, savedCupcake.Price);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetCupcakeByIdAsync_ShouldReturnCupcakeWithDetails()
    {
        // Arrange
        var cupcake = new Cupcake
        {
            Name = "Chocolate",
            BaseFlavor = "Chocolate",
            Price = 4.00m,
            ImageUrl = "https://jaoao.com",
            OrderDetails =
            [
                new OrderDetail { Quantity = 2, Price = 4.00m }
            ],
            CupcakeIngredients =
            [
                new CupcakeIngredient { IngredientId = Guid.NewGuid()}
            ]
        };
        _context.Cupcakes.Add(cupcake);
        await _context.SaveChangesAsync();

        // Act
        var retrievedCupcake = await _cupcakeService.GetCupcakeByIdAsync(cupcake.CupcakeId);

        // Assert
        Assert.NotNull(retrievedCupcake);
        Assert.Single(retrievedCupcake?.OrderDetails!);
        Assert.Single(retrievedCupcake?.CupcakeIngredients!);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task GetAllCupcakesAsync_ShouldReturnAllCupcakes()
    {
        // Arrange
        var cupcakes = new List<Cupcake>
        {
            new() { Name = "Strawberry", BaseFlavor = "Strawberry",
                Price = 3.75m, ImageUrl = "https://jaoao.com" },
            new() { Name = "Lemon", BaseFlavor = "Lemon",
                Price = 3.25m, ImageUrl = "https://jaoao.com" }
        };
        _context.Cupcakes.AddRange(cupcakes);
        await _context.SaveChangesAsync();

        // Act
        var allCupcakes = await _cupcakeService.GetAllCupcakesAsync();

        // Assert
        Assert.Equal(2, allCupcakes.Count());
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task UpdateCupcakeAsync_ShouldUpdateCupcakeInDatabase()
    {
        // Arrange
        var cupcake = new Cupcake
        {
            Name = "Red Velvet",
            BaseFlavor = "Red Velvet",
            Price = 4.50m,
            ImageUrl = "https://jaoao.com"
        };
        _context.Cupcakes.Add(cupcake);
        await _context.SaveChangesAsync();

        // Act
        cupcake.Price = 5.00m;
        await _cupcakeService.UpdateCupcakeAsync(cupcake, []);

        // Assert
        var updatedCupcake = await _context.Cupcakes.FirstOrDefaultAsync(c => c.CupcakeId == cupcake.CupcakeId);
        Assert.NotNull(updatedCupcake);
        Assert.Equal(5.00m, updatedCupcake.Price);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task DeleteCupcakeAsync_ShouldRemoveCupcakeFromDatabase()
    {
        // Arrange
        var cupcake = new Cupcake
        {
            Name = "Blueberry",
            BaseFlavor = "Blueberry",
            Price = 4.00m,
            ImageUrl = "https://jaoao.com"
        };
        _context.Cupcakes.Add(cupcake);
        await _context.SaveChangesAsync();

        // Act
        await _cupcakeService.DeleteCupcakeAsync(cupcake.CupcakeId);

        // Assert
        var deletedCupcake = await _context.Cupcakes.FindAsync(cupcake.CupcakeId);
        Assert.Null(deletedCupcake);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task AddIngredientsToCupcakeAsync_ShouldAddIngredientsToCupcake()
    {
        // Arrange
        var cupcake = new Cupcake
        {
            Name = "Coconut",
            BaseFlavor = "Coconut",
            Price = 3.75m,
            ImageUrl = "https://jaoao.com"
        };
        var ingredient1 = new Ingredient { Name = "Coconut Flakes", Type = "Topping", Availability = true };
        var ingredient2 = new Ingredient { Name = "Vanilla", Type = "Flavoring", Availability = true };
        _context.Cupcakes.Add(cupcake);
        _context.Ingredients.AddRange(ingredient1, ingredient2);
        await _context.SaveChangesAsync();

        // Act
        await _cupcakeService.AddIngredientsToCupcakeAsync(cupcake.CupcakeId,
            [ingredient1.IngredientId, ingredient2.IngredientId]);

        // Assert
        var cupcakeWithIngredients = await _cupcakeService.GetCupcakeByIdAsync(cupcake.CupcakeId);
        Assert.Equal(2, cupcakeWithIngredients?.CupcakeIngredients?.Count);
    }

    [Fact]
    // skipcq: CS-R1073
    public async Task RemoveIngredientFromCupcakeAsync_ShouldRemoveIngredientFromCupcake()
    {
        // Arrange
        var cupcake = new Cupcake { Name = "Almond", BaseFlavor = "Almond", Price = 4.25m, ImageUrl = "https://jaoao.com" };
        var ingredient = new Ingredient { Name = "Almond Extract", Type = "Flavoring", Availability = true };
        var cupcakeIngredient = new CupcakeIngredient { Cupcake = cupcake, Ingredient = ingredient };
        _context.Cupcakes.Add(cupcake);
        _context.Ingredients.Add(ingredient);
        _context.CupcakeIngredients.Add(cupcakeIngredient);
        await _context.SaveChangesAsync();

        // Act
        await _cupcakeService.RemoveIngredientFromCupcakeAsync(cupcake.CupcakeId, ingredient.IngredientId);

        // Assert
        var cupcakeWithoutIngredient = await _cupcakeService.GetCupcakeByIdAsync(cupcake.CupcakeId);
        Assert.Empty(cupcakeWithoutIngredient?.CupcakeIngredients!);
    }
}
