using API.SDK.Infrastructure.MessageBus;
using CoinTracker.Api.CoinList.Infrastructure;
using CoinTracker.Api.CoinList.Infrastructure.Mapper;
using CoinTracker.API.CoinList.Application;
using CoinTracker.API.CoinList.Domain.Entities;
using CoinTracker.API.SDK.Infrastructure.DataMapper;
using CoinTracker.API.SDK.Infrastructure.Providers;
namespace CoinTracker.API.CoinList
{
    public static class CoinListInjector
    {
        public static IServiceCollection AddCoinListServices(this IServiceCollection services)
        {
            services.AddDataMapper<CoinMapperProfile>();
            services.AddMongo();
            services.AddProvider<Coin>();
            services.AddServices();
            services.AddMassTransitWithRabbitMq();
            services.AddPublisher();
            return services;
        }
    }
}
