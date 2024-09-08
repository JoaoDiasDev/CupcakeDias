using CupcakeDias.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = Guid.Parse("cb4e3c76-d337-4f4b-82df-04e9acc4fb74"), RoleName = "Admin" },
            new Role { RoleId = Guid.Parse("bcaa2fbe-9209-46a2-b41c-176815419ab5"), RoleName = "Manager" },
            new Role { RoleId = Guid.Parse("1ed998c4-137d-4bdd-9f34-f397bae620e9"), RoleName = "Customer" }
        );
    }
}