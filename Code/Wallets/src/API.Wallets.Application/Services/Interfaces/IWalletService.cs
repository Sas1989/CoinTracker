using CoinTracker.API.SDK.Application.ApplicationService.Interfaces;
using CoinTracker.API.Wallets.Domain.Dtos;
using CoinTracker.API.Wallets.Domain.Entities;

namespace CoinTracker.API.Wallets.Application.Services.Interfaces
{
    public interface IWalletService : IApplicationService<Wallet,WalletDto,RecivedWalletDto>
    {
    }
}