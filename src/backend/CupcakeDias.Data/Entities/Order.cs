using System;

namespace CupcakeDias.Data.Entities;

public class Order
{
    public int OrderID { get; set; }
    public int UserID { get; set; }
    public DateTime OrderDate { get; set; }
    public required string Status { get; set; }
    public User? User { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}