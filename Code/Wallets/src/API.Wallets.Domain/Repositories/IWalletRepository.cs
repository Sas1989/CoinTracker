using API.SDK.Domain.Repositories;
using API.Wallets.Domain.Entities.WalletEntity;
using API.Wallets.Domain.Entities.WalletEntity.ValueObjects;

namespace API.Wallets.Domain.Repositories;

public interface IWalletRepository : IBaseRepository<Wallet>
{
    public Task AddCoinAsync(Wallet wallet, Guid coinId, decimal numberOfCoin);
    public Task<bool> WalletNameIsUnique(WalletName name);
    public Task<decimal> GetWalletBalance(Wallet wallet);
}
