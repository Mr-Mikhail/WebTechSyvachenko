using Azure.Search.Documents.Models;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Models;

public class DishSearchModel
{
    public string SearchText { get; set; } = default!;

    public SearchResults<Dish> Dishes { get; set; } = default!;
}