using OnlineShop.Domain.Models;

namespace OnlineShop.ViewModels;

public class MapViewModel
{
    public string Key { get; set; } = default!;
    public List<Restaurant> RestaurantLocations { get; set; } = new();
}