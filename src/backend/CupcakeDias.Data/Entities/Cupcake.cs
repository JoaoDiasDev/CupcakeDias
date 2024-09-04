using System;
using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class Cupcake
{
    [Key]
    public Guid CupcakeId { get; set; }
    [StringLength(100)]
    public required string Name { get; set; }
    [StringLength(100)]
    public required string BaseFlavor { get; set; }
    /// <summary>
    /// Base Price for cupcake
    /// </summary>
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public required string ImageUrl { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
    public ICollection<CupcakeIngredient>? CupcakeIngredients { get; set; }
    public ICollection<CartItem>? CartItems { get; set; }
}
