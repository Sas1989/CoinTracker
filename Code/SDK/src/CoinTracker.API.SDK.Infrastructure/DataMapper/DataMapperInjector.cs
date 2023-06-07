using API.SDK.Application.DataMapper;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace API.SDK.Infrastructure.DataMapper
{
    public static class DataMapperInjector
    {
        public static IServiceCollection AddDataMapper<TProfile>(this IServiceCollection services)
        {
            services.AddSingleton<IDataMapper, AutoMapperDataMapper>();
            services.AddAutoMapper(Assembly.GetAssembly(typeof(TProfile)));
            return services;
        }
    }
}
