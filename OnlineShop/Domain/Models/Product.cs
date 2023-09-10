namespace OnlineShop.Domain.Models;

public class Product
{
    public Guid Id { get; set; }

    public string ProductName { get; set; } = default!;
    
    public string ProductDescription { get; set; } = default!;

    public Guid? ProductPhotoId { get; set; }

    public decimal ProductPrice { get; set; }

    public string Currency { get; set; } = default!;

}