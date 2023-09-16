using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModels;

public class DishViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Dish Name is required.")]
    public string Name { get; set; } = default!;
    
    [Required(ErrorMessage = "Dish Description is required.")]
    public string Description { get; set; } = default!;

    public string? PhotoUrl { get; set; }
    
    public string? PhotoName { get; set; }

    [Required(ErrorMessage = "Dish Price is required.")]
    public double Price { get; set; }

    public List<ReviewViewModel> Reviews { get; set; } = new();

    public List<CategoryViewModel> Categories { get; set; } = new();

    [Required(ErrorMessage = "Currency is required.")]
    public string Currency { get; set; } = default!;
}