using CoinTracker.API.Wallets.Application.Services;
using CoinTracker.API.Wallets.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Application
{
    public static class ServicesInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IWalletService, WalletService>();
            return services;
        }
    }

}
