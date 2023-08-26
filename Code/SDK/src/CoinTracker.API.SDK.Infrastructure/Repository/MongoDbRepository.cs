using API.SDK.Application.Repository;
using API.SDK.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SDK.Infrastructure.Repository
{
    public class MongoDbRepository<T> : IRepository<T> where T : Entity
    {
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
        private IMongoCollection<T> Collection { get; set; }

        public MongoDbRepository(IMongoCollection<T> collection)
        {
            this.Collection = collection;
        }
        public async Task<T> CreateAsync(T entity)
        {
            if (entity.Equals(default(T)))
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Collection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<T?> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);

            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAsync<TField>(string field, TField filedValue)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(field, filedValue);
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
            var result = await Collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;

        }

        public async Task<T?> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<T> filter = filterBuilder.Eq(existingentity => existingentity.Id, entity.Id);

            var result = await Collection.ReplaceOneAsync(filter, entity);

            if (!result.IsAcknowledged || result.MatchedCount == 0)
            {
                return null!;
            }

            return entity;
        }

        public async Task<IEnumerable<T>> CreateAsync(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await Collection.InsertManyAsync(entities);
            return entities;
        }
    }
}
