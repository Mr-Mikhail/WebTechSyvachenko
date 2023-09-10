using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.Persistence;

namespace OnlineShop.Application.Services;

public class ProductManagementService : IProductManagementService
{
    private readonly DefaultContext _defaultContext;

    public ProductManagementService(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken token)
    {
        return await _defaultContext.Products.AsNoTracking().ToListAsync(token);
    }

    public async Task<IEnumerable<Product>> GetProducts(Expression<Func<Product, bool>> query)
    {
        return await _defaultContext.Products.Where(query).ToListAsync();
    }

    public async Task<Product> CreateProductAsync(Product product, CancellationToken token)
    {
        await _defaultContext.Products.AddAsync(product, token);
        await _defaultContext.SaveChangesAsync(token);

        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        _defaultContext.Products.Update(product);
        await _defaultContext.SaveChangesAsync();

        return product;
    }

    public async Task<Guid> DeleteProductAsync(Guid id)
    {
        var productToRemove = await _defaultContext.Products.FindAsync(id);

        if (productToRemove == null) 
            return Guid.Empty;
        
        _defaultContext.Products.Remove(productToRemove);
        await _defaultContext.SaveChangesAsync();

        return id;

    }
}