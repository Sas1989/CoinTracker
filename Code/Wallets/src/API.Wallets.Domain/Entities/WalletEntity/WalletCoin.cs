using API.SDK.Domain.Entities;
using API.Wallets.Domain.Entities.CoinEntity;
using API.Wallets.Domain.Entities.CoinEntity.ValueObjects;
using API.Wallets.Domain.Entities.WalletEntity.ValueObjects;

namespace API.Wallets.Domain.Entities.WalletEntity;

public sealed class WalletCoin : Entity
{
    private WalletCoin(Guid id, CoinSymbol symbol, CoinName name, WalletQuantity quantity)  : base(id)
    {
        Symbol = symbol;
        Name = name;
        Quantity = quantity;
    }

    public CoinSymbol Symbol { get; private set; }
    public CoinName Name { get; private set; }
    public WalletQuantity Quantity { get; private set; }

    internal static WalletCoin Create(Coin coin, WalletQuantity Quantity)
    {
        return new WalletCoin( coin.Id, coin.Symbol, coin.Name, Quantity);
    }
}
