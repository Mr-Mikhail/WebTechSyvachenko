using System.ComponentModel.DataAnnotations;
using OnlineShop.Controllers.Api.Category.Dto;
using OnlineShop.Controllers.Api.Dish.Dto;

namespace OnlineShop.ViewModels;

public class DishDataViewModel
{
    [Required]
    public DishViewModel DishView { get; set; } = default!;

    public List<CategoryViewModel> Categories { get; set; } = new();
    
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<Guid> SelectedCategoryIds { get; set; } = new();
}