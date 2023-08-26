using API.SDK.Domain.Entities;
using API.SDK.Domain.ValuesObjects;
using API.Wallets.Domain.ErrorCodes;

namespace API.Wallets.Domain.Entities.WalletEntity.ValueObjects;

public sealed class WalletQuantity : MajorThenZero<decimal>
{
    public WalletQuantity(decimal value) : base(value)
    {
    }

    protected override Error GenerateError()
    {
        return ErrorCode.Wallet.QuantityIsNegative;
    }
}
