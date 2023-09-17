using System.Net;
using Newtonsoft.Json;
using OnlineShop.Application.Models;
using OnlineShop.Controllers;

namespace OnlineShop.Application.Services;

public class Privat24Service
{
    private readonly IHttpClientFactory _httpClientFactory;
    private static string DateNow => DateTime.Now.ToString("dd.MM.yyyy");
    private readonly Uri _exchangeUri = new($"https://api.privatbank.ua/p24api/exchange_rates?date={DateNow}");

    public Privat24Service(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Privat24Model> GetCurrentExchangeRates(CancellationToken token)
    {
        var client = _httpClientFactory.CreateClient(Routes.Privat24);
        
        var response = await client.GetAsync(_exchangeUri, token);
        if (response.StatusCode != HttpStatusCode.OK)
            return new Privat24Model();
        
        var content = await response.Content.ReadAsStringAsync(token);
        var privat24Model = JsonConvert.DeserializeObject<Privat24Model>(content);

        return privat24Model ?? new Privat24Model();
    }
}