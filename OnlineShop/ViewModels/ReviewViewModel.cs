using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModels;

public class ReviewViewModel
{
    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string Value { get; set; } = default!;

    [Required]
    [Range(0, 5)]
    public int Stars { get; set; }
    
    public Guid ProductId { get; set; }
}