using API.SDK.Domain.Entities;
using API.Wallets.Domain.Entities.CoinEntity.ValueObjects;

namespace API.Wallets.Domain.Entities.CoinEntity;

public sealed class Coin : AuditableEntity
{

    public CoinSymbol Symbol { get; private set; }

    public CoinName Name { get; private set; }
    public CoinValue Value { get; private set; }

    private Coin(CoinSymbol symbol, CoinName name, CoinValue value) : base()
    {
        Symbol = symbol;
        Name = name;
        Value = value;
    }

    public static Coin Create(CoinSymbol symbol, CoinName name, CoinValue value)
    {
        return new(symbol, name, value);

    }

}