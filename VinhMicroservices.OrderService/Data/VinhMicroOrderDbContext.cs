using Microsoft.EntityFrameworkCore;
using VinhMicroservices.Model;

namespace VinhMicroservices.OrderService.Data;

public class VinhMicroOrderDbContext : DbContext
{
    public VinhMicroOrderDbContext(DbContextOptions<VinhMicroOrderDbContext> options) : base(options) => Database.EnsureCreated();

    public DbSet<OrderModel> Orders { get; set; }
}
