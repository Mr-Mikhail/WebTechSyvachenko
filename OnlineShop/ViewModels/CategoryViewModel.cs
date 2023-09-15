using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModels;

public class CategoryViewModel
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;
    
    public string? Description { get; set; }
    
    public Guid DishId { get; set; }
}