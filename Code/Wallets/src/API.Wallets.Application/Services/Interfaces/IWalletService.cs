using API.SDK.Application.ApplicationService.Interfaces;
using API.Wallets.Domain.Dtos.Wallet;
using API.Wallets.Domain.Entities.Wallet;

namespace API.Wallets.Application.Services.Interfaces
{
    public interface IWalletService : IApplicationService<Wallet, WalletDto, RecivedWalletDto>
    {
    }
}