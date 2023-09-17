using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.Persistence;

namespace OnlineShop.Application.Repositories;

public class RestaurantRepository : IRepository<Restaurant>
{
    private readonly DefaultContext _defaultContext;

    public RestaurantRepository(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync(CancellationToken token)
    {
        return await _defaultContext.Restaurants.AsNoTracking().ToListAsync(token);
    }

    public Task<IEnumerable<Restaurant>> GetAsync(Expression<Func<Restaurant, bool>> query, IFilteringOptions options, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Restaurant> CreateAsync(Restaurant item, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Restaurant> UpdateAsync(Restaurant item, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> DeleteAsync(Guid id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}