
using API.SDK.Domain.Entities;
using API.SDK.Domain.ValuesObjects;
using API.Wallets.Domain.ErrorCodes;

namespace API.Wallets.Domain.Entities.CoinEntity.ValueObjects;

public sealed class CoinSymbol : StringRequired
{
    public CoinSymbol(string value) : base(value)
    {
    }

    protected override Error GenerateError()
    {
        return ErrorCode.Coin.SimbolIsEmpty;
    }
}
