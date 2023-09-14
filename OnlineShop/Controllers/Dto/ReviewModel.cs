using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Controllers.Dto;

public class ReviewModel
{
    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string Value { get; set; } = default!;

    [Required]
    [Range(0, 5)]
    public int Stars { get; set; }
    
    [Required]
    public Guid DishId { get; set; }
}