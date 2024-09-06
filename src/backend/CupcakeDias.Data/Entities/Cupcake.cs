using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;
    [StringLength(400)]
    public required string ImageUrl { get; set; }
    [JsonIgnore]
    public ICollection<OrderDetail>? OrderDetails { get; set; }
    [JsonIgnore]
    public ICollection<CupcakeIngredient>? CupcakeIngredients { get; set; }
    [JsonIgnore]
    public ICollection<CartItem>? CartItems { get; set; }
}
