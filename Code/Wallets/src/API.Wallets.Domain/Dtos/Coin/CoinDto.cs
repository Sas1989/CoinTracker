using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Domain.Dtos.Coin
{
    public readonly record struct CoinDto(
        Guid Id,
        string Symbol,
        string Name,
        decimal Value);
}