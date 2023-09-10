using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModels;

public class ProductViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Product Name is required.")]
    public string ProductName { get; set; } = default!;
    
    [Required(ErrorMessage = "Product Description is required.")]
    public string ProductDescription { get; set; } = default!;

    // TODO: Handle image in normal way - blob storage / CDN
    public IFormFile? ProductPhoto { get; set; }

    [Required(ErrorMessage = "Product Price is required.")]
    public double ProductPrice { get; set; }

    [Required(ErrorMessage = "Currency is required.")]
    public string Currency { get; set; } = default!;
}