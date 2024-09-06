using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CupcakeDias.Data.Entities;

public class Order
{
    [Key]
    public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    [StringLength(50)]
    public required string Status { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}