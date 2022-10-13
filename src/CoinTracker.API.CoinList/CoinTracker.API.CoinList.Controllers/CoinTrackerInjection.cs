using CoinTracker.Api.CoinList.Infrastructure.Providers;
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
            return services;
        }
    }
}
