using API.SDK.Infrastructure.Cache;
using API.SDK.Infrastructure.Dispatcher;
using API.SDK.Infrastructure.MessageBus;
using API.SDK.Infrastructure.Repositories;
using API.SDK.Infrastructure.Repository;
using API.Wallets.Domain.Entities.CoinEntity;
using API.Wallets.Domain.Entities.WalletEntity;
using System.Reflection;

namespace API.Wallets;

public static class WalletInjector
{
    public static IServiceCollection AddWalletServices(this IServiceCollection services)
    {
        var assembly = new[] {
            Assembly.Load("API.Wallets.Application")
        };

        services.AddMongo();
        services.AddCqrsDispatcher(assembly);
        services.AddProvider<Coin>();
        services.AddMassTransitWithRabbitMq();
        services.AddRepositories();

        services.AddDataProvider<Wallet>();
        services.AddCache<Wallet>();

        services.AddDataProvider<Coin>();
        services.AddCache<Coin>();


        return services;
    }
}
