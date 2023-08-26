using API.SDK.Application.Repository;
using API.SDK.Domain.Entities;
using API.SDK.Domain.Persistence.DataProvider;
using API.SDK.Infrastructure.DataProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace API.SDK.Infrastructure.Repository;

public static class ProvidersInjection
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {

        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
        services.AddSingleton(provider =>
        {
            var configuration = GetService<IConfiguration>(provider);

            var host = configuration["MongoDb:host"];
            var port = configuration["MongoDb:port"];
            var database = configuration["MongoDb:database"];

            if(string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(database))
            {
                throw new ArgumentException("Mongodb configuration not set check Host port and Database");
            }

            string connectionstring = $"mongodb://{host}:{port}";
            return new MongoClient(connectionstring).GetDatabase(database);
        });

        return services;
    }

    public static IServiceCollection AddProvider<T>(this IServiceCollection serivices) where T : Entity
    {
        serivices.AddSingleton((Func<IServiceProvider, IRepository<T>>)(provider =>
        {
            IMongoDatabase mongoDb = GetService<IMongoDatabase>(provider);
            var collection = typeof(T).Name;
            var mongoClient = mongoDb.GetCollection<T>(collection);
            return new MongoDbRepository<T>(mongoClient);


        }));

        return serivices;
    }

    public static IServiceCollection AddDataProvider<T>(this IServiceCollection serivices) where T : Entity
    {
        serivices.AddSingleton<IDataProvider<T>, MongoDbDataProvider<T>>();
        return serivices;
    }

    private static T GetService<T>(IServiceProvider provider)
    {
        var service = provider.GetService<T>();
        if (service == null)
        {
            throw new ArgumentException(nameof(service), $"{typeof(T)} not registerd correct in the service manager.");
        }

        return service;
    }
}
