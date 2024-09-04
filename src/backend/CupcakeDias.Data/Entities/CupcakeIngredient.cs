using System;
using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class CupcakeIngredient
{
    [Key]
    public Guid CupcakeIngredientId { get; set; }
    public Guid CupcakeId { get; set; }
    public Guid IngredientId { get; set; }
    public Cupcake? Cupcake { get; set; }
    public Ingredient? Ingredient { get; set; }
}