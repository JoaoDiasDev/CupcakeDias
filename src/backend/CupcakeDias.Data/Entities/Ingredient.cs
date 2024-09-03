using System;

namespace CupcakeDias.Data.Entities;

public class Ingredient
{
    public int IngredientID { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public bool Availability { get; set; }
    public ICollection<CupcakeIngredient>? CupcakeIngredients { get; set; }
}