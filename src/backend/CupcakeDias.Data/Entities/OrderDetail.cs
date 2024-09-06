using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CupcakeDias.Data.Entities;

public class OrderDetail
{
    [Key]
    public Guid OrderDetailId { get; set; }
    public int Quantity { get; set; }
    /// <summary>
    /// Price after changes and discounts
    /// </summary>
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public Guid OrderId { get; set; }
    [JsonIgnore]
    public Order? Order { get; set; }
    public Guid CupcakeId { get; set; }
    public Cupcake? Cupcake { get; set; }
}
