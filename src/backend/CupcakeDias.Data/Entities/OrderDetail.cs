using System;

namespace CupcakeDias.Data.Entities;

public class OrderDetail
{
    public int OrderDetailID { get; set; }
    public int OrderID { get; set; }
    public int CupcakeID { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Order? Order { get; set; }
    public Cupcake? Cupcake { get; set; }
}
