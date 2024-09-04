using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class CartItem
{
    [Key]
    public Guid CartItemId { get; set; }
    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public Guid CartId { get; set; }
    public Cart? Cart { get; set; }

    public Guid? CupcakeId { get; set; }
    public Cupcake? Cupcake { get; set; }


}
