using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Models;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.Persistence;

namespace OnlineShop.Application.Repositories;

public class ReviewRepository : IRepository<Review>
{
    private readonly DefaultContext _defaultContext;

    public ReviewRepository(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<IEnumerable<Review>> GetAllAsync(CancellationToken token)
    {
        return await _defaultContext.Reviews.AsNoTracking().ToListAsync(token);
    }

    public async Task<IEnumerable<Review>> GetAsync(Expression<Func<Review, bool>> query, IFilteringOptions? options, CancellationToken token)
    {       
        var result = _defaultContext.Reviews.Include(x => x.Dish).Where(query);

        if (options == null || options is FilteringOptions specificOptions == false)
            return await result.ToListAsync(token);

        if (specificOptions.Pagination != null)
            result = result.Skip(specificOptions.Pagination.Skip).Take(specificOptions.Pagination.Take);

        return await result.ToListAsync(token);
    }

    public async Task<Review> CreateAsync(Review item, CancellationToken token)
    {
        await _defaultContext.Reviews.AddAsync(item, token);
        await _defaultContext.SaveChangesAsync(token);

        return item;
    }

    public async Task<Review> UpdateAsync(Review item, CancellationToken token)
    {
        _defaultContext.Reviews.Update(item);
        await _defaultContext.SaveChangesAsync(token);

        return item;
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken token)
    {
        var reviewToRemove = await _defaultContext.Reviews.FindAsync(id);

        if (reviewToRemove == null) 
            return Guid.Empty;
        
        _defaultContext.Reviews.Remove(reviewToRemove);
        await _defaultContext.SaveChangesAsync(token);

        return id;
    }
}