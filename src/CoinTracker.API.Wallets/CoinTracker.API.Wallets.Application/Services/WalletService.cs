using CoinTracker.API.SDK.Application.ApplicationService;
using CoinTracker.API.SDK.Application.DataMapper;
using CoinTracker.API.SDK.Application.IProvider;
using CoinTracker.API.Wallets.Application.Services.Interfaces;
using CoinTracker.API.Wallets.Domain.Dtos;
using CoinTracker.API.Wallets.Domain.Entities;

namespace CoinTracker.API.Wallets.Application.Services
{
    public class WalletService : ApplicationService<Wallet,WalletDto,RecivedWalletDto>, IWalletService
    {
        public WalletService(IProvider<Wallet> provider, IDataMapper mapper) : base(provider, mapper)
        {
        }
    }
}