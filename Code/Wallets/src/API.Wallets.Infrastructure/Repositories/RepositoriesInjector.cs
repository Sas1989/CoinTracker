using API.Wallets.Domain.Repositories;
using API.Wallets.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace API.SDK.Infrastructure.Repositories
{
    public static class RepositoriesInjector
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IWalletRepository, WalletRepository>();

            return services;
        }
    }
}
