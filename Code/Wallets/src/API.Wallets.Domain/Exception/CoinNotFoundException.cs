using API.SDK.Domain.Entities;
using API.SDK.Domain.Exceptions;
using API.Wallets.Domain.ErrorCodes;

namespace API.Wallets.Domain.Exception;

public class CoinNotFoundException : BaseApplicationException
{
    public CoinNotFoundException() : base(ErrorCode.Coin.NotFound)
    {
    }
}
