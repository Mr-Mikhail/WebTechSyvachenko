using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Models;

namespace OnlineShop.Persistence;

public class DefaultContext : DbContext
{
    public DefaultContext(DbContextOptions<DefaultContext> options)
        : base(options)
    {
    }

    public DbSet<Dish> Products { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>().ToTable("Photos");
        
        modelBuilder.Entity<Dish>()
            .HasOne(p => p.Photo)
            .WithOne(p => p.Dish)
            .HasForeignKey<Photo>(p => p.ProductId);

        modelBuilder.Entity<Review>()
            .HasOne(p => p.Dish)
            .WithMany(p => p.Reviews)
            .HasForeignKey(p => p.ProductId);
        
        modelBuilder.Entity<Category>()
            .HasOne(p => p.Dish)
            .WithMany(p => p.Categories)
            .HasForeignKey(p => p.ProductId);
        
        base.OnModelCreating(modelBuilder);
    }
}