using API.SDK.Domain.Entities;
using API.SDK.Domain.Persistence.DataProvider;
using Microsoft.Extensions.Caching.Memory;
using SharpCompress.Common;
using System.Linq.Expressions;

namespace API.SDK.Infrastructure.Cache;

internal class CachedDataProvider<T> : IDataProvider<T> where T : Entity
{
    private readonly IMemoryCache _cache;
    private readonly IDataProvider<T> _dataProvider;

    public CachedDataProvider(IDataProvider<T> dataProvider, IMemoryCache cache)
    {
        _cache = cache;
        _dataProvider = dataProvider;
    }


    public Task<long> Count() => _dataProvider.Count();

    public Task<long> Count(Expression<Func<T, bool>> condition) => _dataProvider.Count(condition);

    public async Task DeleteAsync(Guid id)
    {
        await _dataProvider.DeleteAsync(id);

        RemoveFromCache(id);
    }

    public Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> condition) => _dataProvider.FilterAsync(condition);

    public Task<IEnumerable<T>> GetAllAsync() => _dataProvider.GetAllAsync();

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _cache.GetOrCreateAsync(id, async entry => { return await _dataProvider.GetByIdAsync(id); });
    }

    public async Task SaveAsync(T entity)
    {
        await _dataProvider.SaveAsync(entity);

        AddToCache(entity);
    }

    public async Task SaveAsync(IEnumerable<T> entityList)
    {
        await _dataProvider.SaveAsync(entityList);

        foreach(T entity in entityList)
        {
            AddToCache(entity);
        }
    }

    private void AddToCache(T entity)
    {
        _cache.Set(entity.Id, entity);
    }

    private void RemoveFromCache(Guid id)
    {
        _cache.Remove(id);
    }
}
