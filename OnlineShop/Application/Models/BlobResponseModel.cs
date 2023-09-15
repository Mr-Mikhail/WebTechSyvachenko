namespace OnlineShop.Application.Models;

public class BlobResponseModel
{
    public string? Status { get; set; }
    
    public bool Error { get; set; }
    
    public BlobModel Blob { get; set; } = new();
}