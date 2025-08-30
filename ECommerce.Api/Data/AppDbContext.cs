using ECommerce.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Basit seed veriler
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop",  UnitPrice = 30000m, Stock = 10 },
            new Product { Id = 2, Name = "Telefon", UnitPrice = 15000m, Stock = 20 },
            new Product { Id = 3, Name = "Kulaklık",UnitPrice = 1200m,  Stock = 50 }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, FullName = "Hakan Sağlam", Email = "hakan@example.com" },
            new Customer { Id = 2, FullName = "Ada Yılmaz",   Email = "ada@example.com" }
        );
    }
}