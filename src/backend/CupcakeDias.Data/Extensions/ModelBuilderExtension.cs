using System;
using CupcakeDias.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = Guid.NewGuid(), RoleName = "Admin" },
            new Role { RoleId = Guid.NewGuid(), RoleName = "Manager" },
            new Role { RoleId = Guid.NewGuid(), RoleName = "Customer" }
        );
    }
}