using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.Persistence;

namespace OnlineShop.Application.Repositories;

public class ProductRepository : IRepository<Dish>
{
    private readonly DefaultContext _defaultContext;

    public ProductRepository(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<IEnumerable<Dish>> GetAllAsync(CancellationToken token)
    {
        return await _defaultContext.Products.Include(x => x.Reviews).Include(x => x.Photo).AsNoTracking().ToListAsync(token);
    }

    public async Task<IEnumerable<Dish>> GetAsync(Expression<Func<Dish, bool>> query, CancellationToken token)
    {
        return await _defaultContext.Products.Include(x => x.Reviews).Include(x => x.Photo).Where(query).ToListAsync(token);
    }

    public async Task<Dish> CreateAsync(Dish item, CancellationToken token)
    {
        await _defaultContext.Products.AddAsync(item, token);
        await _defaultContext.SaveChangesAsync(token);

        return item;
    }

    public async Task<Dish> UpdateAsync(Dish item, CancellationToken token)
    {
        _defaultContext.Products.Update(item);
        await _defaultContext.SaveChangesAsync(token);

        return item;
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken token)
    {
        var productToRemove = await _defaultContext.Products.FindAsync(id);

        if (productToRemove == null) 
            return Guid.Empty;
        
        _defaultContext.Products.Remove(productToRemove);
        await _defaultContext.SaveChangesAsync(token);

        return id;
    }
}