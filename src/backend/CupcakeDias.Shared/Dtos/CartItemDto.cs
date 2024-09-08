namespace CupcakeDias.Shared.Dtos;

public class CartItemDto
{
    public Guid CartItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Guid CupcakeId { get; set; }
}