using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.CoinList.Acceptance.Support.Wallets.Models
{
    internal record struct WalletCoinsInput(
        Guid coinId,
        decimal numberOfCoin
    );
}
