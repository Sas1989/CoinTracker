using CoinTracker.API.CoinList.Acceptance.Support.CoinList.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.CoinList
{
    public class CoinAction : BaseAction<Coin>
    {
        public CoinAction() : base(CoinListEndPoint.API_COIN, CoinListEndPoint.API_COIN_BULK)
        {
        }

        public async Task<Coin> Create(string symbol, string name, decimal value)
        {
            return await Create(new RecivedCoin { Name = name, Symbol = symbol, Value = value });
        }
    }
}
