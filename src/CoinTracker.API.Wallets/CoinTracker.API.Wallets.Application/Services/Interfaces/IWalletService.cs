using CoinTracker.API.Wallets.Domain.Dtos;

namespace CoinTracker.API.Wallets.Application.Services.Interfaces
{
    public interface IWalletService
    {
        Task<WalletDto> CreateAsync(RecivedWalletDto recivedWallet);
        Task<WalletDto?> GetWalletAsync(Guid walletId);
        Task<IEnumerable<WalletDto>> GetWalletAsync();
        Task<bool> DeleteWalletAsync(Guid walletId);
    }
}