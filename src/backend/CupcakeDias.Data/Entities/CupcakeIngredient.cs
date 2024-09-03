using System;
using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class CupcakeIngredient
{
    [Key]
    public int CupcakeIngredientId { get; set; }
    public int CupcakeId { get; set; }
    public int IngredientId { get; set; }
    public Cupcake? Cupcake { get; set; }
    public Ingredient? Ingredient { get; set; }
}