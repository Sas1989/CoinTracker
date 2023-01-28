using CoinTracker.API.SDK.Application.DataMapper;
using CoinTracker.API.SDK.Application.IProvider;
using CoinTracker.API.Wallets.Application.Services.Interfaces;
using CoinTracker.API.Wallets.Domain.Dtos;
using CoinTracker.API.Wallets.Domain.Entities;

namespace CoinTracker.API.Wallets.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IProvider<Wallet> provider;
        private readonly IDataMapper dataMapper;

        public WalletService(IProvider<Wallet> provider, IDataMapper dataMapper)
        {
            this.provider = provider;
            this.dataMapper = dataMapper;
        }

        public async Task<WalletDto> CreateAsync(RecivedWalletDto recivedWallet)
        {
            Wallet wallet = dataMapper.Map<Wallet>(recivedWallet);
            var saveWallet = await provider.CreateAsync(wallet);
            return dataMapper.Map<WalletDto>(saveWallet);  
        }

        public async Task<bool> DeleteWalletAsync(Guid walletId)
        {
            return await provider.DeleteAsync(walletId); ;
        }

        public async Task<WalletDto?> GetWalletAsync(Guid walletId)
        {
            var wallet = await provider.GetAsync(walletId);
            return dataMapper.Map<WalletDto>(wallet); ;
        }

        public async Task<IEnumerable<WalletDto>> GetWalletAsync()
        {
            var wallets = await provider.GetAllAsync();
            return dataMapper.Map<IEnumerable<WalletDto>>(wallets);
        }
    }
}