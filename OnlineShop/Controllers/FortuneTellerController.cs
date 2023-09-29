using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Models;

namespace OnlineShop.Controllers;

[Route("fortune-teller")]
public class FortuneTellerController : Controller
{
    private static readonly List<FortuneModel> Fortunes = new();
    
    [HttpPost("add-fortune")]
    public async Task<IActionResult> AddFortune([FromBody] FortuneModel fortune)
    {
        Fortunes.Add(fortune);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetFortune()
    {
        return View(Fortunes);
    }
}