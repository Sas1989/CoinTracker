using CoinTracker.Api.CoinList.Infrastructure.Publishers;
using CoinTracker.API.CoinList.Application.Common.Publishers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoinTracker.Api.CoinList.Infrastructure
{

    public static class ProvidersInjection
    {


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

            services.AddSingleton<ICoinPublisher, RabbitMqPublisher>();
            return services;
        }
    }
}
