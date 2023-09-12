using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Domain.Models;

public class Dish
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = default!;
    
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = default!;

    public Photo? Photo { get; set; }

    public double Price { get; set; }

    public List<Category> Categories { get; set; } = new();

    public bool IsAvailable { get; set; } = true;

    public string Currency { get; set; } = default!;

    public List<Review> Reviews { get; set; } = new();
}