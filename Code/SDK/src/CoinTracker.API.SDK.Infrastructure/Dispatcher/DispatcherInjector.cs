using API.SDK.Application.Dispatcher;
using API.Wallets.Application;
using API.Wallets.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace API.SDK.Infrastructure.Dispatcher;

public static class DispatcherInjector
{
    public static IServiceCollection AddCqrsDispatcher(this IServiceCollection services, Assembly[] assembly)
    {

        services.AddSingleton<IDispatcher, CqrsDispatcher>();
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AddClasses(c=> c.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        return services;
    }
}
