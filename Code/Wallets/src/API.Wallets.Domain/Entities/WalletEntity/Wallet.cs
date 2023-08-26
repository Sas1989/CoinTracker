using API.SDK.Domain.Entities;
using API.Wallets.Domain.Entities.CoinEntity;
using API.Wallets.Domain.Entities.WalletEntity.ValueObjects;
using API.Wallets.Domain.Exception;

namespace API.Wallets.Domain.Entities.WalletEntity;

public sealed class Wallet : AuditableEntity
{
    private readonly List<WalletCoin> _coins;
    private Wallet(WalletName name, string? description) : base()
    {
        Name = name;
        Description = description;
        _coins = new();
    }
    public WalletName Name { get; private set; }
    public string? Description { get; private set; }
    public IReadOnlyList<WalletCoin> Coins => _coins;

    public static Wallet Create(WalletName name, string? description)
    {
        return new(name, description);
    }

    public void AddNewCoin(Coin coin, WalletQuantity Quantity)
    {
        if(CoinIsInTheWallet(coin))
        {
            throw new CoinAlreadyInTheWallet();
        }

        var coinWallet = WalletCoin.Create(coin, Quantity);
        _coins.Add(coinWallet);
        ChangeUpdateTime();
    }

    private bool CoinIsInTheWallet(Coin coin)
    {
        return _coins.Any(c => c.Id == coin.Id);
    }
}
