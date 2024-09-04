using System;
using System.ComponentModel.DataAnnotations;

namespace CupcakeDias.Data.Entities;

public class Role
{
    [Key]
    public Guid RoleId { get; set; }

    [StringLength(50)]
    public required string RoleName { get; set; }

    public ICollection<User>? Users { get; set; }
}

