using API.SDK.Domain.Entities;
using System.Linq.Expressions;

namespace API.SDK.Domain.Persistence.DataProvider;

public interface IDataProvider<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> condition);
    Task<long> Count();
    Task<long> Count(Expression<Func<TEntity, bool>> condition);

    Task SaveAsync(TEntity entity);
    Task SaveAsync(IEnumerable<TEntity> entityList);
    Task DeleteAsync(Guid id);

}
