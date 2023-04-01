using CoinTracker.API.SDK.Application.IProvider;
using CoinTracker.API.SDK.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CoinTracker.API.SDK.Infrastructure.Providers
{
    public static class ProvidersInjection
    {
        public static IServiceCollection AddMongo<T>(this IServiceCollection services) where T : Entity
        {

            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            services.AddSingleton<IProvider<T>>(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                string connectionstring = $"mongodb://{configuration["MongoDb:host"]}:{configuration["MongoDb:port"]}";
                string database = configuration["MongoDb:database"];
                string collection = configuration["MongoDb:collection"];
                var mongoClient = new MongoClient(connectionstring).GetDatabase(database).GetCollection<T>(collection);
                return new MongoDbProvider<T>(mongoClient);
            });

            return services;
        }
    }
}
