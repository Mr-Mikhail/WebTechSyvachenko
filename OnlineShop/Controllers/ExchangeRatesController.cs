using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services;

namespace OnlineShop.Controllers;

public class ExchangeRatesController : Controller
{
    private readonly Privat24Service _privat24Service;

    public ExchangeRatesController(Privat24Service privat24Service)
    {
        _privat24Service = privat24Service;
    }

    public async Task<IActionResult> Index(CancellationToken token)
    {
        var exchangeRates = await _privat24Service.GetCurrentExchangeRates(token);
        if (exchangeRates.Date == null || exchangeRates.ExchangeRate == null)
            return RedirectToAction("Index", "Dish");
        
        return View(exchangeRates);
    }
}