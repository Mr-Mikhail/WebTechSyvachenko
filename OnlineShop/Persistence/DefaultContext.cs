using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Models;

namespace OnlineShop.Persistence;

public class DefaultContext : DbContext
{
    public DefaultContext(DbContextOptions<DefaultContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>().ToTable("Photos");
        
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductPhoto)
            .WithOne(p => p.Product)
            .HasForeignKey<Photo>(p => p.ProductId);

        modelBuilder.Entity<Review>()
            .HasOne(p => p.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(p => p.ProductId);
        
        base.OnModelCreating(modelBuilder);
    }
}