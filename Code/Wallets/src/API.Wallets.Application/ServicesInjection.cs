using API.Wallets.Application.Services;
using API.Wallets.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Application
{
    public static class ServicesInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IWalletService, WalletService>();
            services.AddSingleton<ICoinService, CoinService>();
            return services;
        }
    }

}
