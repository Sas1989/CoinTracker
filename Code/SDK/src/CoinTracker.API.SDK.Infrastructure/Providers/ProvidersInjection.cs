using API.SDK.Application.Provider;
using API.SDK.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace API.SDK.Infrastructure.Providers
{
    public static class ProvidersInjection
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {

            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                string connectionstring = $"mongodb://{configuration["MongoDb:host"]}:{configuration["MongoDb:port"]}";
                string database = configuration["MongoDb:database"];
                return new MongoClient(connectionstring).GetDatabase(database);
            });

            return services;
        }

        public static IServiceCollection AddProvider<T>(this IServiceCollection serivices) where T : Entity
        {
            serivices.AddSingleton<IProvider<T>>(provider =>
            {
                var mongoDb = provider.GetService<IMongoDatabase>();
                var collection = typeof(T).Name;
                var mongoClient = mongoDb.GetCollection<T>(collection);
                return new MongoDbProvider<T>(mongoClient);


            });

            return serivices;
        }
    }
}
