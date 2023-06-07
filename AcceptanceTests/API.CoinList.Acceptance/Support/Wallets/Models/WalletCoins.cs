using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.CoinList.Acceptance.Support.Wallets.Models
{
    internal record struct WalletCoins(
        Guid coinId,
        string symnol,
        string description,
        decimal numberOfCoin
    );
}
