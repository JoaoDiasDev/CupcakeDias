using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CupcakeDias.Data.Entities;

public class Cart
{
    [Key]
    public Guid CartId { get; set; }
    [StringLength(50)]
    public required string Status { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [JsonIgnore]

    public ICollection<CartItem>? CartItems { get; set; }
}
