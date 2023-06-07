using API.CoinList.Application.Common.Publishers;
using API.CoinList.Infrastructure.Publishers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.CoinList.Infrastructure
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
