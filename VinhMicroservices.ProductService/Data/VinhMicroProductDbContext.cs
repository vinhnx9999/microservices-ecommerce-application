using Microsoft.EntityFrameworkCore;
using VinhMicroservices.Model;

namespace VinhMicroservices.ProductService.Data;

public class VinhMicroProductDbContext : DbContext
{
    public VinhMicroProductDbContext(DbContextOptions<VinhMicroProductDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id = 1, Name = "Laptop", Quantity = 160, Price = 823 });
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id = 2, Name = "iPhone 17 Pro 256GB", Quantity = 100, Price = 950 });
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id = 3, Name = "Samsung Galaxy Z Fold7 5G 12GB/256GB", Quantity = 100, Price = 1007 });
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<ProductModel> Products { get; set; }
}