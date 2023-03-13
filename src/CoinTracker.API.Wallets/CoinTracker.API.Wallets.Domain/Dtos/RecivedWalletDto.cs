using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.Wallets.Domain.Dtos
{
    public readonly record struct RecivedWalletDto(
        string Name,
        string Description);
}
