using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.Models;

public class Photo
{
    [Key]
    public Guid Id { get; set; }
    
    [StringLength(200)]
    public string? Metadata { get; set; }

    public Guid DishId { get; set; }

    public Dish Dish { get; set; } = default!;
}