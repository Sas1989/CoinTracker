using API.SDK.Domain.Entities;
using API.SDK.Domain.Persistence.DataProvider;
using API.SDK.Domain.Repositories;

namespace API.SDK.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
{
    protected readonly IDataProvider<TEntity> _dataProvider;

    protected BaseRepository(IDataProvider<TEntity> dataProvider)
    {
        _dataProvider = dataProvider;
    }
    public Task DeleteAsync(TEntity entity)
    {
        return DeleteByIdAsync(entity.Id);
    }

    public Task DeleteByIdAsync(Guid id)
    {
        return _dataProvider.DeleteAsync(id);
    }

    public Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return _dataProvider.GetAllAsync();
    }

    public Task<TEntity?> GetByIdAsync(Guid id)
    {
        return _dataProvider.GetByIdAsync(id);
    }

    public Task Save(TEntity entity)
    {
        if(Validation(entity)) {

            return _dataProvider.SaveAsync(entity);
        }

        return Task.CompletedTask;
    }

    protected virtual bool Validation(TEntity entity)
    {
        return true;
    }
}
