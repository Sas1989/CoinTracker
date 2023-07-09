using API.CoinList.Domain.Entities;
using API.CoinList.Infrastructure;
using API.CoinList.Infrastructure.Mapper;
using API.CoinList.Infrastructure.Services;
using API.SDK.Infrastructure.DataMapper;
using API.SDK.Infrastructure.MessageBus;
using API.SDK.Infrastructure.Repository;

namespace API.CoinList
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
