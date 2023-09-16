using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.Models;

public class Photo
{
    [Key]
    [Required]
    public string Name { get; set; } = default!;
    
    [StringLength(200)]
    public string? Metadata { get; set; }

    public Guid DishId { get; set; }

    public Dish Dish { get; set; } = default!;
}