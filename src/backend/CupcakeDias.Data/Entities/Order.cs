using System;
using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    [StringLength(50)]
    public required string Status { get; set; }
    public User? User { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}