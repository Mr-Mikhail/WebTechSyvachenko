namespace OnlineShop.Controllers.Api.Dish.Dto;

public class DishApiRequest
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public string Description { get; set; } = default!;
    
    public double Price { get; set; }
    
    public bool IsAvailable { get; set; } = true;

    public string Currency { get; set; } = default!;
}