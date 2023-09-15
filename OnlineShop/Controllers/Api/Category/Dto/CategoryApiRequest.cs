namespace OnlineShop.Controllers.Api.Category.Dto;

public class CategoryApiRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }
}
