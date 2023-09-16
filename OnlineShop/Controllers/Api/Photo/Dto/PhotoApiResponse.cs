namespace OnlineShop.Controllers.Api.Photo.Dto;

public class PhotoApiResponse
{
    public string Name { get; set; } = default!;
    
    public string? Metadata { get; set; }
}