using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.Persistence;

namespace OnlineShop.Application.Repositories;

public class PhotoRepository : IRepository<Photo>
{
    private readonly DefaultContext _defaultContext;

    public PhotoRepository(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<IEnumerable<Photo>> GetAllAsync(CancellationToken token)
    {
        return await _defaultContext.Photos.Include(x => x.Dish).AsNoTracking().ToListAsync(token);
    }

    public async Task<IEnumerable<Photo>> GetAsync(Expression<Func<Photo, bool>> query, IFilteringOptions options, CancellationToken token)
    {
        return await _defaultContext.Photos.Include(x => x.Dish).AsNoTracking().Where(query).ToListAsync(token);
    }

    public async Task<Photo> CreateAsync(Photo item, CancellationToken token)
    {
        await _defaultContext.Photos.AddAsync(item, token);
        await _defaultContext.SaveChangesAsync(token);
        return item;
    }

    public async Task<Photo> UpdateAsync(Photo item, CancellationToken token)
    {
        _defaultContext.Photos.Update(item);
        await _defaultContext.SaveChangesAsync(token);
        return item;
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken token)
    {
        var photoToRemove = await _defaultContext.Photos.FindAsync(id);

        if (photoToRemove == null)
            return Guid.Empty;

        _defaultContext.Photos.Remove(photoToRemove);
        await _defaultContext.SaveChangesAsync(token);

        return id;
    }
}