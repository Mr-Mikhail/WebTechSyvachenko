using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Models;

namespace OnlineShop.Persistence;

public class DefaultContext : IdentityDbContext<IdentityUser>
{
    public DefaultContext(DbContextOptions<DefaultContext> options)
        : base(options)
    {
    }

    public DbSet<Dish> Dishes { get; set; } = default!;
    public DbSet<Photo> Photos { get; set; } = default!;
    public DbSet<Review> Reviews { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Restaurant> Restaurants { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>().ToTable("Photos");
        modelBuilder.Entity<Restaurant>().ToTable("Restaurants");
        
        modelBuilder.Entity<Dish>()
            .HasOne(p => p.Photo)
            .WithOne(p => p.Dish)
            .HasForeignKey<Photo>(p => p.DishId);

        modelBuilder.Entity<Review>()
            .HasOne(p => p.Dish)
            .WithMany(p => p.Reviews)
            .HasForeignKey(p => p.DishId);

        modelBuilder.Entity<Category>()
            .HasIndex(u => u.Name)
            .IsUnique();
        
        modelBuilder.Entity<Category>()
            .HasMany(p => p.Dishes)
            .WithMany(p => p.Categories);
        
        base.OnModelCreating(modelBuilder);
    }
}