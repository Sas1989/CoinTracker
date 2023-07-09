using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Domain.Dtos.Wallet
{
    public readonly record struct WalletDto(
        Guid Id,
        string Name,
        string Description,
        string TotalValue,
        IEnumerable<WalletCoinDto> Coins
     );
}
