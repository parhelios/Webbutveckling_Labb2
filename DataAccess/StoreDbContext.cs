using Microsoft.EntityFrameworkCore;
using StoreApp.Shared.Models;

namespace StoreApp.DataAccess;

public class StoreDbContext : DbContext
{
    public DbSet<ContactInfo> ContactInfo { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ProductsOrders> ProductOrders { get; set; }

    public StoreDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductsOrders>()
            .HasKey(po => new { po.OrderId, po.ProductId });

        modelBuilder.Entity<ProductsOrders>()
            .HasOne(sc => sc.Product)
            .WithMany(s => s.Orders)
            .HasForeignKey(sc => sc.ProductId);

        modelBuilder.Entity<ProductsOrders>()
            .HasOne(sc => sc.Order)
            .WithMany(c => c.Products)
            .HasForeignKey(sc => sc.OrderId);
    }
}