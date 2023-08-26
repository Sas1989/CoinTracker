using API.SDK.Domain.Entities;
using API.SDK.Domain.Persistence.DataProvider;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Linq.Expressions;

namespace API.SDK.Infrastructure.DataProviders;

public class MongoDbDataProvider<TEntity> : IDataProvider<TEntity> where TEntity : Entity
{
    private IMongoCollection<TEntity> Collection { get; set; }
    public MongoDbDataProvider(IMongoDatabase mongoDatabase)
    {
        var collectionName = typeof(TEntity).Name;
        this.Collection = mongoDatabase.GetCollection<TEntity>(collectionName);
    }
    public async Task<long> Count()
    {
        var filter = CreateEmptyFilter();
        return await Collection.CountDocumentsAsync(filter);
    }

    public async Task<long> Count(Expression<Func<TEntity, bool>> condition)
    {
        var filter = CreateFilterByCondition(condition);
        return await Collection.CountDocumentsAsync(filter);
    }

    public async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> condition)
    {
        var filter = CreateFilterByCondition(condition);
        return await Collection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var emptyFilter = CreateEmptyFilter();
        return await Collection.Find(emptyFilter).ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        var filerById = CreateFilterByID(id);
        return await Collection.Find(filerById).FirstOrDefaultAsync();
    }
    public Task SaveAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var filter = CreateFilterByID(entity.Id);
        return Collection.ReplaceOneAsync(filter, entity);
    }

    public Task SaveAsync(IEnumerable<TEntity> entityList)
    {
        ArgumentNullException.ThrowIfNull(entityList);

        var operationList = new List<WriteModel<TEntity>>();
        foreach (var entity in entityList)
        {
            var filter = CreateFilterByID(entity.Id);
            var operation = new ReplaceOneModel<TEntity>(filter, entity) { IsUpsert = true };
            operationList.Add(operation);

        }

        return Collection.BulkWriteAsync(operationList);
    }

    public Task DeleteAsync(Guid id)
    {
        var filter = CreateFilterByID(id);
        return Collection.DeleteOneAsync(filter);
    }

    private static FilterDefinition<TEntity> CreateFilterByCondition(Expression<Func<TEntity, bool>> condition)
    {
        return Builders<TEntity>.Filter.Where(condition);
    }
    private static FilterDefinition<TEntity> CreateEmptyFilter()
    {
        return Builders<TEntity>.Filter.Empty;
    }
    private static FilterDefinition<TEntity> CreateFilterByID(Guid id)
    {
        return Builders<TEntity>.Filter.Eq(entity => entity.Id, id);
    }
}
