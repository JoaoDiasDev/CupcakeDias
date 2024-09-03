using System;
using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public int CupcakeId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Order? Order { get; set; }
    public Cupcake? Cupcake { get; set; }
}
