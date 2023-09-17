namespace OnlineShop.Application.Models;

public class Privat24Model
{
    public string? Date { get; set; }
    public string? Bank { get; set; }
    public int BaseCurrency { get; set; }
    public string? BaseCurrencyLit { get; set; }
    public ExchangeRate[]? ExchangeRate { get; set; }
}

public class ExchangeRate
{
    public string? BaseCurrency { get; set; }
    public string? Currency { get; set; }
    public double SaleRateNb { get; set; }
    public double PurchaseRateNb { get; set; }
    public double SaleRate { get; set; }
    public double PurchaseRate { get; set; }
}