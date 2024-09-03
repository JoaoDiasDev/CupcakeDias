using System;
using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class Cart
{
    [Key]
    public int CartId { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
    [StringLength(50)]
    public required string Status { get; set; }

    public ICollection<CartItem>? CartItems { get; set; }
}
