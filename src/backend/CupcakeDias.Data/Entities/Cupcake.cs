using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    [JsonIgnore]
    public ICollection<OrderDetail>? OrderDetails { get; set; }
    [JsonIgnore]
    public ICollection<CupcakeIngredient>? CupcakeIngredients { get; set; }
    [JsonIgnore]
    public ICollection<CartItem>? CartItems { get; set; }
}
