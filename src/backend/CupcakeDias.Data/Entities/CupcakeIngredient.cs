using System;

namespace CupcakeDias.Data.Entities;

public class CupcakeIngredient
{
    public int CupcakeIngredientID { get; set; }
    public int CupcakeID { get; set; }
    public int IngredientID { get; set; }
    public Cupcake? Cupcake { get; set; }
    public Ingredient? Ingredient { get; set; }
}