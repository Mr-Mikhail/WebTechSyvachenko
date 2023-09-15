using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Models;

public class FilteringOptions : IFilteringOptions
{
    public FilteringOptions()
    {
        
    }

    public FilteringOptions(PaginationModel pagination)
    {
        Pagination = pagination;
    }
    
    public bool AsNoTracking { get; set; }

    public PaginationModel? Pagination { get; set; }
    public static FilteringOptions AsNoTrackingInstance => new() { AsNoTracking = true };
}