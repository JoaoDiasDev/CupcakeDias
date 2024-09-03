using CupcakeDias.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CupcakeDias.Data;

public class CupcakeDiasContext(DbContextOptions<CupcakeDiasContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Cupcake> Cupcakes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<CupcakeIngredient> CupcakeIngredients { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=cupcake-dias;Uid=developer;Pwd=Matheus321*;",
            ServerVersion.AutoDetect("Server=localhost;Port=3306;Database=cupcake-dias;Uid=developer;Pwd=Matheus321*;"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cupcake)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CupcakeId);

        modelBuilder.Entity<CupcakeIngredient>()
            .HasKey(ci => ci.CupcakeIngredientId);

        modelBuilder.Entity<CupcakeIngredient>()
            .HasOne(ci => ci.Cupcake)
            .WithMany(c => c.CupcakeIngredients)
            .HasForeignKey(ci => ci.CupcakeId);

        modelBuilder.Entity<CupcakeIngredient>()
            .HasOne(ci => ci.Ingredient)
            .WithMany(i => i.CupcakeIngredients)
            .HasForeignKey(ci => ci.IngredientId);
    }
}