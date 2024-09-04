using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class OrderDetail
{
    [Key]
    public Guid OrderDetailId { get; set; }
    public int Quantity { get; set; }
    /// <summary>
    /// Price after changes and discounts
    /// </summary>
    public decimal Price { get; set; }
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    public Guid CupcakeId { get; set; }
    public Cupcake? Cupcake { get; set; }
}
