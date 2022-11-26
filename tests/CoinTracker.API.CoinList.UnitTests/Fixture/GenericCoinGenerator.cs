using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.UnitTests.Fixture
{
    public static class GenericCoinGenerator
    {
        private static readonly List<GenericCoin> coins = GenerateCoin();
        private static List<GenericCoin> GenerateCoin()
        {
            List<GenericCoin> genericCoins = new List<GenericCoin>();

            genericCoins.Add(new()
            {
                Id = Guid.Parse("1a049978-32a8-45b6-95d9-24b7847a534b"),
                Symbol = "BTC",
                Name = "Bitcoin",
                Value = 20000
            });

            genericCoins.Add(new()
            {
                Id = Guid.Parse("dd6dbd88-d005-45a3-98f0-aff222c45a07"),
                Symbol = "ETH",
                Name = "Etherium",
                Value = 1800
            });


            return genericCoins;

        }

        public static List<GenericCoin> Generate()
        {
            return coins;
        }
    }
}
