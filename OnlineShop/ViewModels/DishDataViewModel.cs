using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModels;

public class DishDataViewModel
{
    [Required]
    public DishViewModel DishView { get; set; } = default!;

    public List<CategoryViewModel> Categories { get; set; } = new();
    
    public IFormFile? Photo { get; set; }
    
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<Guid> SelectedCategoryIds { get; set; } = new();
}