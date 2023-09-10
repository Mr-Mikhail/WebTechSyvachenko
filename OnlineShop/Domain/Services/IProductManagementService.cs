using System.Linq.Expressions;
using OnlineShop.Domain.Models;

namespace OnlineShop.Domain.Services;

public interface IProductManagementService
{
    public Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken token);

    public Task<IEnumerable<Product>> GetProducts(Expression<Func<Product, bool>> query);

    public Task<Product> CreateProductAsync(Product product, CancellationToken token);

    public Task<Product> UpdateProductAsync(Product product);

    public Task<Guid> DeleteProductAsync(Guid id);
}