using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Domain.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string ProductName { get; set; } = default!;
    
    [Required]
    [StringLength(500)]
    public string ProductDescription { get; set; } = default!;

    public Photo? ProductPhoto { get; set; }

    public double ProductPrice { get; set; }

    public string Currency { get; set; } = default!;

    public List<Review> Reviews { get; set; } = new();
}