using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Models;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.Persistence;

namespace OnlineShop.Application.Repositories;

public class CategoryRepository : IRepository<Category>
{
    private readonly DefaultContext _defaultContext;

    public CategoryRepository(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken token)
    {
        return await _defaultContext.Categories.AsNoTracking().ToListAsync(token);
    }

    public async Task<IEnumerable<Category>> GetAsync(Expression<Func<Category, bool>> query, IFilteringOptions? options, CancellationToken token)
    {
        var result = _defaultContext.Categories.Include(x => x.Dishes).Where(query);
        
        if (options == null || options is FilteringOptions specificOptions == false)
            return await result.ToListAsync(token);

        if (specificOptions.AsNoTracking)
            result = result.AsNoTracking();

        return await result.ToListAsync(token);
    }

    public async Task<Category> CreateAsync(Category item, CancellationToken token)
    {
        await _defaultContext.Categories.AddAsync(item, token);
        await _defaultContext.SaveChangesAsync(token);

        return item;
    }

    public async Task<Category> UpdateAsync(Category item, CancellationToken token)
    {
        _defaultContext.Categories.Update(item);
        await _defaultContext.SaveChangesAsync(token);

        return item;
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken token)
    {
        var reviewToRemove = await _defaultContext.Categories.FindAsync(id);

        if (reviewToRemove == null) 
            return Guid.Empty;
        
        _defaultContext.Categories.Remove(reviewToRemove);
        await _defaultContext.SaveChangesAsync(token);

        return id;
    }
}