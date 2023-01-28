using AutoMapper;
using CoinTracker.API.SDK.Application.DataMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoinTracker.API.SDK.Infrastructure.DataMapper
{
    public static class DataMapperInjector
    {
        public static IServiceCollection AddDataMapper<TProfile>(this IServiceCollection services)
        {
            services.AddScoped<IDataMapper, AutoMapperDataMapper>();
            services.AddAutoMapper(Assembly.GetAssembly(typeof(TProfile)));
            return services;
        }
    }
}
