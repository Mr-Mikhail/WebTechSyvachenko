using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.Persistence;

namespace OnlineShop.Application.Services;

public class ProductDatabaseService : IDatabaseService<Product>
{
    private readonly DefaultContext _defaultContext;

    public ProductDatabaseService(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken token)
    {
        return await _defaultContext.Products.Include(x => x.Reviews).Include(x => x.ProductPhoto).AsNoTracking().ToListAsync(token);
    }

    public async Task<IEnumerable<Product>> GetAsync(Expression<Func<Product, bool>> query, CancellationToken token)
    {
        return await _defaultContext.Products.Include(x => x.Reviews).Include(x => x.ProductPhoto).Where(query).ToListAsync(token);
    }

    public async Task<Product> CreateAsync(Product item, CancellationToken token)
    {
        await _defaultContext.Products.AddAsync(item, token);
        await _defaultContext.SaveChangesAsync(token);

        return item;
    }

    public async Task<Product> UpdateAsync(Product item, CancellationToken token)
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