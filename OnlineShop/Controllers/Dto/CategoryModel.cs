using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Controllers.Dto;

public class CategoryModel
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;
    
    public string? Description { get; set; }
    
    public Guid DishId { get; set; }
}