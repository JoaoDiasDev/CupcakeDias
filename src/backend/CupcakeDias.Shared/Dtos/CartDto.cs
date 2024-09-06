namespace CupcakeDias.Shared.Dtos;

public class CartDto
{
    public Guid CartId { get; set; }
    public required string Status { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<CartItemDto> CartItems { get; set; } = [];
}