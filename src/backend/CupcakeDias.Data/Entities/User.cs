using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    [StringLength(100)]
    public required string Email { get; set; }
    [StringLength(50)]
    public required string PhoneNumber { get; set; }
    [StringLength(100)]
    public required string Name { get; set; }
    [StringLength(255)]
    public required string PasswordHash { get; set; }
    [StringLength(255)]
    public required string Address { get; set; }
    [StringLength(355)]
    public string? Token { get; set; }
    public required Guid RoleId { get; set; }
    public Role? Role { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Cart>? Carts { get; set; }
}
