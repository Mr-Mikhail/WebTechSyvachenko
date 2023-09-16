using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers;

public class MapController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}