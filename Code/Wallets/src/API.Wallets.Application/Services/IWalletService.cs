using API.SDK.Application.ApplicationService;
using API.Wallets.Domain.Dtos.Wallet;
using API.Wallets.Domain.Entities.Wallet;

namespace API.Wallets.Application.Services
{
    public interface IWalletService : IApplicationService<Wallet, WalletDto, WalletDtoInput>
    {
        Task<WalletDto> AddCoin(Guid walletId, WalletCoinDtoInput recivedWalletCoinDto);
    }
}