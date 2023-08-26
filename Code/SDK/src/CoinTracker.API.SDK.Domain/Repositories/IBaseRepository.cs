using API.SDK.Domain.Entities;

namespace API.SDK.Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : Entity
{
    public Task<IEnumerable<TEntity>> GetAllAsync();
    public Task<TEntity?> GetByIdAsync(Guid id);
    public Task Save(TEntity entity);
    public Task DeleteAsync(TEntity entity);
    public Task DeleteByIdAsync(Guid id);
}
