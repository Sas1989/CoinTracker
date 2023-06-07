using API.SDK.Application.ApplicationService;
using API.SDK.Application.DataMapper;
using API.SDK.Application.Provider;
using API.Wallets.Application.Services.Interfaces;
using API.Wallets.Domain.Dtos.Wallet;
using API.Wallets.Domain.Entities.Wallet;

namespace API.Wallets.Application.Services
{
    public class WalletService : ApplicationService<Wallet, WalletDto, RecivedWalletDto>, IWalletService
    {
        public WalletService(IProvider<Wallet> provider, IDataMapper mapper) : base(provider, mapper)
        {
        }
    }
}