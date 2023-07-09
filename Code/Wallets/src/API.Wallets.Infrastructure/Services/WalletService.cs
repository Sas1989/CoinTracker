using API.SDK.Application.DataMapper;
using API.SDK.Application.Repository;
using API.SDK.Infrastructure.Services;
using API.Wallets.Application.Services;
using API.Wallets.Domain.Dtos.Wallet;
using API.Wallets.Domain.Entities.Wallet;

namespace API.Wallets.Infrastructure.Services
{
    public class WalletService : ApplicationService<Wallet, WalletDto, WalletDtoInput>, IWalletService
    {
        public WalletService(IRepository<Wallet> repository, IDataMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<WalletDto> AddCoin(Guid walletId, WalletCoinDtoInput recivedWalletCoinDto)
        {
            var wallet = await repository.GetAsync(walletId);

            if(wallet == default)
            {
                return default;
            }
            var walletCoin = mapper.Map<WalletCoin>(recivedWalletCoinDto);

            wallet.Coins.Add(walletCoin);

            var walletSaved = await repository.UpdateAsync(wallet);

            return mapper.Map<WalletDto>(walletSaved);
        }
    }
}