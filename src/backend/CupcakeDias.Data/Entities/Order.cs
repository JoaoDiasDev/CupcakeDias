using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CupcakeDias.Data.Entities;

public class Order
{
    [Key]
    public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    [StringLength(50)]
    public required string Status { get; set; }
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public required decimal Total { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}