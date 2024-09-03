using System;
using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class CartItem
{
    [Key]
    public int CartItemId { get; set; }

    public int CartId { get; set; }
    public Cart? Cart { get; set; }

    public int? CupcakeId { get; set; }
    public Cupcake? Cupcake { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
