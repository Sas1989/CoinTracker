using CoinTracker.Api.CoinList.Infrastructure.Publishers;
using CoinTracker.API.CoinList.Application.Common.Publishers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoinTracker.Api.CoinList.Infrastructure
{

    public static class ProvidersInjection
    {


        public static IServiceCollection AddPublisher(this IServiceCollection services)
        {
            services.AddTransient<ICoinPublisher, RabbitMqPublisher>();
            return services;
        }
    }
}
