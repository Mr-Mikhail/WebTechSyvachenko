using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services;

namespace OnlineShop.Controllers.Api.Privat24;

[Route(Routes.Privat24)]
public class Privat24Controller : ControllerBase
{
    private readonly Privat24Service _privat24Service;

    public Privat24Controller(Privat24Service privat24Service)
    {
        _privat24Service = privat24Service;
    }

    [HttpGet]
    public async Task<IActionResult> GetExchangeRates(CancellationToken token)
    {
        return Ok(await _privat24Service.GetCurrentExchangeRates(token));
    }
}