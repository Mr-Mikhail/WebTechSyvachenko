using System.Linq.Expressions;
using OnlineShop.Domain.Models;

namespace OnlineShop.Domain.Services;

public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken token);

    public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> query, IFilteringOptions options, CancellationToken token);

    public Task<T> CreateAsync(T item, CancellationToken token);

    public Task<T> UpdateAsync(T item, CancellationToken token);

    public Task<Guid> DeleteAsync(Guid id, CancellationToken token);
}