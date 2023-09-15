using System.ComponentModel.DataAnnotations;
using OnlineShop.Controllers.Api.Category.Dto;
using OnlineShop.Controllers.Api.Review.Dto;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers.Api.Dish.Dto;

public class DishModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Dish Name is required.")]
    public string Name { get; set; } = default!;
    
    [Required(ErrorMessage = "Dish Description is required.")]
    public string Description { get; set; } = default!;

    // TODO: Handle image in normal way - blob storage / CDN
    public IFormFile? Photo { get; set; }

    [Required(ErrorMessage = "Dish Price is required.")]
    public double Price { get; set; }

    public List<ReviewModel> Reviews { get; set; } = new();

    public List<CategoryViewModel> Categories { get; set; } = new();

    [Required(ErrorMessage = "Currency is required.")]
    public string Currency { get; set; } = default!;
}