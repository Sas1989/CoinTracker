using API.SDK.Infrastructure.DataMapper;
using API.SDK.Infrastructure.MessageBus;
using API.SDK.Infrastructure.Repository;
using API.Wallets.Domain.Entities;
using API.Wallets.Domain.Entities.Wallet;
using API.Wallets.Infrastructure.Mapper;
using API.Wallets.Infrastructure.Services;

namespace API.Wallets
{
    public static class WalletInjector
    {
        public static IServiceCollection AddWalletServices(this IServiceCollection services)
        {
            services.AddDataMapper<WalletMapperProfile>();
            services.AddMongo();
            services.AddProvider<Wallet>();
            services.AddProvider<Coin>();
            services.AddServices();
            services.AddMassTransitWithRabbitMq();
            return services;
        }
    }
}
