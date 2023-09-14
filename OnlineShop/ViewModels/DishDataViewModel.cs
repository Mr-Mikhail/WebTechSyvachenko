using System.ComponentModel.DataAnnotations;
using OnlineShop.Controllers.Dto;

namespace OnlineShop.ViewModels;

public class DishDataViewModel
{
    [Required]
    public DishModel Dish { get; set; } = default!;

    public List<CategoryModel> Categories { get; set; } = new();
    
    public List<Guid> SelectedCategoryIds { get; set; } = new();
}