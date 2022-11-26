using CoinTracker.Api.CoinList.Infrastructure.Mapper;
using CoinTracker.Api.CoinList.Infrastructure.Providers;
using CoinTracker.Api.CoinList.Infrastructure.Publishers;
using CoinTracker.API.CoinList.Application.Common.Mappers;
using CoinTracker.API.CoinList.Application.Common.Providers;
using CoinTracker.API.CoinList.Application.Common.Publishers;
using CoinTracker.API.CoinList.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.Api.CoinList.Infrastructure
{

    public static class ProvidersInjection
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {

            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            services.AddScoped<IProvider>(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                string connectionstring = $"mongodb://{configuration["MongoDb:host"]}:{configuration["MongoDb:port"]}";
                string database = configuration["MongoDb:database"];
                string collection = configuration["MongoDb:collection"];
                var mongoClient = new MongoClient(connectionstring).GetDatabase(database).GetCollection<Coin>(collection);
                return new MongoDbProvider(mongoClient);
            });

            return services;
        }
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddScoped<IDataMapper, AutoMapperDataMapper>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {

            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((provider, configurator) =>
                {
                    var configuration = provider.GetService<IConfiguration>();
                    configurator.Host(configuration["RabbitMqSettings:host"]);
                    configurator.ConfigureEndpoints(provider, new KebabCaseEndpointNameFormatter(configuration["RabbitMqSettings:queueName"], false));
                    configurator.UseMessageRetry(retryConfigurator =>
                    {
                        retryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
                    });
                });
            });

            services.AddScoped<ICoinPublisher, RabbitMqPublisher>();
            return services;
        }
    }
}
