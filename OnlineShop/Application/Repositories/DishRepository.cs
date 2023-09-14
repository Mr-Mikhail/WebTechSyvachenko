using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Models;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.Persistence;

namespace OnlineShop.Application.Repositories;

public class DishRepository : IRepository<Dish>
{
    private readonly DefaultContext _defaultContext;

    public DishRepository(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<IEnumerable<Dish>> GetAllAsync(CancellationToken token)
    {
        return await _defaultContext.Dishes.Include(x => x.Reviews).Include(x => x.Photo).Include(x => x.Categories).AsNoTracking().ToListAsync(token);
    }

    public async Task<IEnumerable<Dish>> GetAsync(Expression<Func<Dish, bool>> query, IFilteringOptions? options, CancellationToken token)
    {
        var result = _defaultContext.Dishes.Include(x => x.Reviews).Include(x => x.Photo)
            .Include(x => x.Categories).Where(query);

        if (options == null || options is FilteringOptions specificOptions == false)
            return await result.ToListAsync(token);

        if (specificOptions.AsNoTracking)
            result = result.AsNoTracking();
            
        return await result.ToListAsync(token);
    }

    public async Task<Dish> CreateAsync(Dish item, CancellationToken token)
    {
        await _defaultContext.Dishes.AddAsync(item, token);
        await _defaultContext.SaveChangesAsync(token);

        return item;
    }

    public async Task<Dish> UpdateAsync(Dish item, CancellationToken token)
    {
        _defaultContext.Dishes.Update(item);
        await _defaultContext.SaveChangesAsync(token);

        return item;
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken token)
    {
        var dishToRemove = await _defaultContext.Dishes.FindAsync(id);

        if (dishToRemove == null) 
            return Guid.Empty;
        
        _defaultContext.Dishes.Remove(dishToRemove);
        await _defaultContext.SaveChangesAsync(token);

        return id;
    }
}