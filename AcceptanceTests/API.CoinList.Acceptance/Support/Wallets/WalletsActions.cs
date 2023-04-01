using CoinTracker.API.CoinList.Acceptance.Support.Services;
using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions;
using CoinTracker.API.CoinList.Acceptance.Support.Wallets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.Wallets
{
    public class WalletsActions : BaseAction<Wallet>
    {
        public WalletsActions() : base(WalletsEndPoint.API_WALLETS, WalletsEndPoint.API_WALLETS_BULK)
        {
        }
    }
}
