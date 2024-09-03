using System;

namespace CupcakeDias.Data.Entities;

public class User
{
    public int UserID { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Name { get; set; }
    public required string PasswordHash { get; set; }
    public required string Address { get; set; }
    public ICollection<Order>? Orders { get; set; }
}
