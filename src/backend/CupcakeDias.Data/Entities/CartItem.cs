using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CupcakeDias.Data.Entities;

public class CartItem
{
    [Key]
    public Guid CartItemId { get; set; }
    public int Quantity { get; set; }
    /// <summary>
    /// Price after changes and discounts
    /// </summary>
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public Guid CartId { get; set; }
    [JsonIgnore]
    public Cart? Cart { get; set; }

    public Guid CupcakeId { get; set; }
    public Cupcake? Cupcake { get; set; }


}
