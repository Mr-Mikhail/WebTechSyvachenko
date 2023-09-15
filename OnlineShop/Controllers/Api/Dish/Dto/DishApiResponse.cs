using OnlineShop.Controllers.Api.Category.Dto;
using OnlineShop.Controllers.Api.Review.Dto;

namespace OnlineShop.Controllers.Api.Dish.Dto;

public class DishApiResponse
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public string Description { get; set; } = default!;

    public Domain.Models.Photo? Photo { get; set; }

    public double Price { get; set; }

    public List<CategoryApiResponse> Categories { get; set; } = new();

    public bool IsAvailable { get; set; } = true;

    public string Currency { get; set; } = default!;

    public List<ReviewApiResponse> Reviews { get; set; } = new();
}