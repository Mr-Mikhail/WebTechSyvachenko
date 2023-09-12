using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Domain.Models;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    public Guid ProductId { get; set; }

    public Dish Dish { get; set; } = default!;
}