using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CupcakeDias.Data.Entities;

public class Role
{
    [Key]
    public Guid RoleId { get; set; }

    [StringLength(50)]
    public required string RoleName { get; set; }
    [JsonIgnore]

    public ICollection<User>? Users { get; set; }
}

