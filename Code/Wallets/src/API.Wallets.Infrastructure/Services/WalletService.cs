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

        Task<WalletDto> IWalletService.AddCoin(Guid walletId, WalletCoinDtoInput recivedWalletCoinDto)
        {
            throw new NotImplementedException();
        }
    }
}