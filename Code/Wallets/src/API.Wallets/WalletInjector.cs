﻿using API.SDK.Infrastructure.MessageBus;
using API.Wallets.Domain.Entities;
using CoinTracker.API.CoinList.Application;
using CoinTracker.API.SDK.Infrastructure.DataMapper;
using CoinTracker.API.SDK.Infrastructure.Providers;
using CoinTracker.API.Wallets.Domain.Entities;
using CoinTracker.API.Wallets.Infrastructure.Mapper;

namespace CoinTracker.API.CoinList
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
