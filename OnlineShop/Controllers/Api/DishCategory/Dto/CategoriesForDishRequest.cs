using JetBrains.Annotations;

namespace OnlineShop.Controllers.Api.DishCategory.Dto;

public class CategoriesForDishRequest
{
    public Guid DishId { get; set; }
        
    [UsedImplicitly]
    public List<Guid> CategoryIds { get; set; } = new();

}