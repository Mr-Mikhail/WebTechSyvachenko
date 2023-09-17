using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineShop.Application.Configurations;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;

public class MapController : Controller
{
    private readonly IRepository<Restaurant> _restaurantRepository;
    private readonly IOptions<MapConfiguration> _options;

    public MapController(IRepository<Restaurant> restaurantRepository, IOptions<MapConfiguration> options)
    {
        _restaurantRepository = restaurantRepository;
        _options = options;
    }

    public async Task<IActionResult> Index(CancellationToken token)
    {
        var restaurants = (await _restaurantRepository.GetAllAsync(token)).ToList();
        return View(new MapViewModel
        {
            RestaurantLocations = restaurants,
            Key = _options.Value.Key
        });
    }
}