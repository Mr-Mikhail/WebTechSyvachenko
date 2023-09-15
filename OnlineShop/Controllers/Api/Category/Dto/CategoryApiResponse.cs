using Microsoft.Build.Framework;

namespace OnlineShop.Controllers.Api.Category.Dto;

public class CategoryApiResponse
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;
    
    public string? Description { get; set; }
}