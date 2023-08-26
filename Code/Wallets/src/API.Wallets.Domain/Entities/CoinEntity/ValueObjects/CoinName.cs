using API.SDK.Domain.Entities;
using API.SDK.Domain.ValuesObjects;
using API.Wallets.Domain.ErrorCodes;

namespace API.Wallets.Domain.Entities.CoinEntity.ValueObjects;

public sealed class CoinName : StringRequired
{
    public CoinName(string value) : base(value)
    {
    }

    protected override Error GenerateError()
    {
        return ErrorCode.Coin.NameIsEmpty;
    }
}
