using CoinTracker.API.SDK.Application.IProvider;
using CoinTracker.API.SDK.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.SDK.Infrastructure.Providers
{
    public class MongoDbProvider<T> : IProvider<T> where T : Entity
    {
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
        private IMongoCollection<T> collection { get; set; }

        public MongoDbProvider(IMongoCollection<T> collection)
        {
            this.collection = collection;
        }
        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await collection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);

            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAsync<TField>(string field, TField filedValue)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(field, filedValue);
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
            var result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<T> filter = filterBuilder.Eq(existingentity => existingentity.Id, entity.Id);

            var result = await collection.ReplaceOneAsync(filter, entity);

            if (!result.IsAcknowledged || result.MatchedCount == 0)
            {
                return null;
            }

            return entity;
        }

        public async Task<IEnumerable<T>> CreateAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await collection.InsertManyAsync(entities);
            return entities;
        }
    }
}
