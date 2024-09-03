using CupcakeDias.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Data;

public class CupcakeDiasContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Cupcake> Cupcakes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<CupcakeIngredient> CupcakeIngredients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=cupcake-dias;Uid=developer;Pwd=Matheus321*;",
            ServerVersion.AutoDetect("Server=localhost;Port=3306;Database=cupcake-dias;Uid=developer;Pwd=Matheus321*;"));
    }
}