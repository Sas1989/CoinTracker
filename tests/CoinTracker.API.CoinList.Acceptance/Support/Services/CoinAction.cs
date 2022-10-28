using CoinTracker.API.CoinList.Acceptance.Support.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.Services
{
    public static class CoinAction
    {
        private static HttpClient GetClient()
        {
            return new HttpClientFactory().Build(); 
        }
        public static async Task<Guid> CreateNewCoinAsync(RecivedCoin coin)
        {
            HttpClient client = GetClient();
            var result = await client.PostAsJsonAsync(CoinListEndPoint.API_COIN, coin);
            var coinSaved = await result.Content.ReadFromJsonAsync<Coin>();
            return coinSaved.Id;
        }

        public static async Task<Guid> CreateNewCoinAsync(string symbol, string name, decimal value)
        {
            return await CreateNewCoinAsync(new RecivedCoin { Name = name, Symbol = symbol, Value = value });
        }

       public static async Task CreateMassiveCoinAsync(List<RecivedCoin> coins)
        {
            HttpClient client = GetClient();
            var result = await client.PostAsJsonAsync(CoinListEndPoint.API_COIN_BULK, coins);
        }

        public static async Task<Coin> GetCoin(Guid id)
        {
         
            HttpClient client = GetClient();
            var result = await client.GetAsync($"{CoinListEndPoint.API_COIN}/{id}");
            return await result.Content.ReadFromJsonAsync<Coin>();
        }
    }
}
