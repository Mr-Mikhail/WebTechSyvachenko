using System.Linq.Expressions;

namespace OnlineShop.Domain.Services;

public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken token);

    public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> query, CancellationToken token);

    public Task<T> CreateAsync(T item, CancellationToken token);

    public Task<T> UpdateAsync(T item, CancellationToken token);

    public Task<Guid> DeleteAsync(Guid id, CancellationToken token);
}