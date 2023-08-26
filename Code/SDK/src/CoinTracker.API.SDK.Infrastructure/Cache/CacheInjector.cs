using API.SDK.Domain.Entities;
using API.SDK.Domain.Persistence.DataProvider;
using Microsoft.Extensions.DependencyInjection;

namespace API.SDK.Infrastructure.Cache;

public static class CacheInjector
{
    public static IServiceCollection AddCache<T>(this IServiceCollection services) where T : Entity
    {
        services.AddMemoryCache();

        services.Decorate<IDataProvider<T>, CachedDataProvider<T>>();
        return services;
    }
}
