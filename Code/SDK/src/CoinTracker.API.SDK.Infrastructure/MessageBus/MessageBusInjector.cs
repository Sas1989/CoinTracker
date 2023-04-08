using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace API.SDK.Infrastructure.MessageBus
{
    public static class MessageBusInjector
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {

            services.AddMassTransit(configure =>
            {
                configure.AddConsumers(Assembly.GetEntryAssembly());
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

            return services;
        }
    }
}
