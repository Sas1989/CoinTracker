using API.SDK.Application.Repository;
using API.SDK.Domain.Persistence.DataProvider;
using API.SDK.Infrastructure.Repositories;
using API.Wallets.Domain.Entities.CoinEntity;
using API.Wallets.Domain.Entities.WalletEntity;
using API.Wallets.Domain.Entities.WalletEntity.ValueObjects;
using API.Wallets.Domain.Exception;
using API.Wallets.Domain.Repositories;

namespace API.Wallets.Infrastructure.Repositories;

public sealed class WalletRepository : BaseRepository<Wallet>, IWalletRepository
{
    private readonly IRepository<Coin> _coinRepository;

    public WalletRepository(IDataProvider<Wallet> dataProvider, IRepository<Coin> coinRepository) : base(dataProvider)
    {
        _coinRepository = coinRepository;
    }

    public async Task AddCoinAsync(Wallet wallet, Guid coinId, decimal numberOfCoin)
    {
        var coin = await _coinRepository.GetAsync(coinId) ?? throw new CoinNotFoundException();

        var quantity = new WalletQuantity(numberOfCoin);
        wallet.AddNewCoin(coin, quantity);

        await _dataProvider.SaveAsync(wallet);
    }

    public Task<decimal> GetWalletBalance(Wallet wallet)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> WalletNameIsUnique(WalletName name)
    {
        var countWalletName = await _dataProvider.Count(wallet => wallet.Name == name);
        return countWalletName  == 0;
    }
}
