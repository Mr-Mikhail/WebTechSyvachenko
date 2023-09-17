using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.Models;

public class Restaurant
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = default!;
    
    [Required]
    public double Latitude { get; set; } = default!;
    
    [Required]
    public double Longitude { get; set; } = default!;
}