namespace OnlineShop.Controllers.Api.Review.Dto;

public class ReviewApiResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Value { get; set; } = default!;

    public int Stars { get; set; }
    
    public Guid DishId { get; set; }
}