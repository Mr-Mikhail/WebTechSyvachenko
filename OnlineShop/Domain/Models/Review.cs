using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Domain.Models;

public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string Value { get; set; } = default!;

    [Required]
    [Range(0, 5)]
    public int Stars { get; set; }
    
    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;
}