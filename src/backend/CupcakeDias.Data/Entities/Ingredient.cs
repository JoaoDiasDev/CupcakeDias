using System;
using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class Ingredient
{
    [Key]
    public int IngredientId { get; set; }
    [StringLength(100)]
    public required string Name { get; set; }
    [StringLength(50)]
    public required string Type { get; set; }
    public bool Availability { get; set; }
    public ICollection<CupcakeIngredient>? CupcakeIngredients { get; set; }
}