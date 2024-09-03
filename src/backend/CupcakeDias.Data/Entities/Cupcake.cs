using System;

namespace CupcakeDias.Data.Entities;

public class Cupcake
{
    public int CupcakeID { get; set; }
    public required string Name { get; set; }
    public required string BaseFlavor { get; set; }
    public decimal Price { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
    public ICollection<CupcakeIngredient>? CupcakeIngredients { get; set; }
}
