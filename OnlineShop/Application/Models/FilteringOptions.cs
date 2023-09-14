using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Models;

public class FilteringOptions : IFilteringOptions
{
    public bool AsNoTracking { get; set; }

    public static FilteringOptions AsNoTrackingInstance => new() { AsNoTracking = true };
}