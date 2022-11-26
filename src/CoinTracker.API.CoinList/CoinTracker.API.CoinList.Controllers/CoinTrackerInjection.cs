using CoinTracker.Api.CoinList.Infrastructure;
using CoinTracker.API.CoinList.Application;

namespace CoinTracker.API.CoinList.Controllers
{
    public static class CoinTrackerInjection
    {
        public static IServiceCollection AddCoinListServices(this IServiceCollection services)
        {
            services.AddMapper();
            services.AddMongo();
            services.AddServices();
            services.AddMassTransitWithRabbitMq();
            return services;
        }
    }
}
