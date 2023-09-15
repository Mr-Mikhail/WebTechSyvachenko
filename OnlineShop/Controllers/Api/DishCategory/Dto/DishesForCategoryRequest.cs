using JetBrains.Annotations;

namespace OnlineShop.Controllers.Api.DishCategory.Dto;

public class DishesForCategoryRequest
{
    public Guid CategoryId { get; set; }
        
    [UsedImplicitly]
    public List<Guid> DishIds { get; set; } = new();

}