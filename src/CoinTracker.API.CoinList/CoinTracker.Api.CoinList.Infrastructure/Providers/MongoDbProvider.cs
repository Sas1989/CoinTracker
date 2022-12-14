using CoinTracker.API.CoinList.Application.Common.Providers;
using CoinTracker.API.CoinList.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.Api.CoinList.Infrastructure.Providers
{
    public class MongoDbProvider : IProvider
    {
        private readonly FilterDefinitionBuilder<Coin> filterBuilder = Builders<Coin>.Filter;
        private IMongoCollection<Coin> collection { get; set; }

        public MongoDbProvider(IMongoCollection<Coin> collection)
        {
            this.collection = collection;
        }
        public async Task<Coin> CreateAsync(Coin entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<Coin>> GetAllAsync()
        {
            return await collection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Coin> GetAsync(Guid id)
        {
            FilterDefinition<Coin> filter = filterBuilder.Eq(entity => entity.Id, id);

            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Coin>> GetAsync<TField>(string field, TField filedValue)
        {
            FilterDefinition<Coin> filter = filterBuilder.Eq(field, filedValue);
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            FilterDefinition<Coin> filter = filterBuilder.Eq(entity => entity.Id, id);
            var result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<Coin> UpdateAsync(Coin entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<Coin> filter = filterBuilder.Eq(existingentity => existingentity.Id, entity.Id);

            var result = await collection.ReplaceOneAsync(filter, entity);

            if (!result.IsAcknowledged || result.MatchedCount == 0)
            {
                return null;
            }

            return entity;
        }

        public async Task<IEnumerable<Coin>> CreateAsync(IEnumerable<Coin> entities)
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
