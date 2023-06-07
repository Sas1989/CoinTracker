using API.CoinList.Application.Services;
using API.CoinList.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.CoinList.Application
{
    public static class ServicesInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICoinService, CoinService>();
            return services;
        }
    }

}
