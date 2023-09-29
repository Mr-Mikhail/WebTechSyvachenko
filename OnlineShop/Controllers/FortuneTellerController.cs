using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Models;

namespace OnlineShop.Controllers;

[Route("fortune-teller")]
public class FortuneTellerController : Controller
{
    private static readonly Dictionary<string, DateTime> Fortunes = new();
    
    [HttpPost("add-fortune")]
    public async Task<IActionResult> AddFortune([FromBody] FortuneModel fortune)
    {
        Fortunes.Add(fortune.Name, DateTime.Parse(fortune.Date));
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetFortune()
    {
        return View(Fortunes);
    }
}