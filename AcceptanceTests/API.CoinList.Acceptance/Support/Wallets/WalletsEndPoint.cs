using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.Wallets
{
    public static class WalletsEndPoint
    {
        public const string API_WALLETS = "api/wallet";
        public const string API_WALLETS_BULK = "api/wallet/bulk";
        public const string API_WALLETS_COINS = "api/wallet/{0}/coins";
    }
}
