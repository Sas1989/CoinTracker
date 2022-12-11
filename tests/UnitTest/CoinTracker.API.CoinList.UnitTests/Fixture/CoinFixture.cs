using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.Domain.Entities;
using CoinTracker.API.Contracts.Coin;
using CoinTracker.API.Contracts.Coins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.UnitTests.Fixture
{
    internal static class CoinFixture
    {
        private static List<GenericCoin> genericCoins = GenericCoinGenerator.Generate();

        private static List<Coin> coins = BuildCoinList();
        private static List<CoinDto> coinsDto = BuildCoinDtosList();
        private static List<RecivedCoinDto> recivedCoinDtos = BuildRecivedDtosList();

        private static List<CoinUpdate> coinsUpdate = BuildCoinUpdateList();
        private static List<CoinInsert> coinsInsert = BuildCoinInsertList();
        private static List<CoinDelete> coinsDelete = BuildCoinDeleteList();

        internal static IEnumerable<CoinUpdate> GenereteListOfCoinUpdate()
        {
            return coinsUpdate;
        }

        internal static IEnumerable<CoinInsert> GenereteListOfCoinInsert()
        {
            return coinsInsert;
        }

        internal static IEnumerable<CoinDelete> GenereteListOfCoinDelete()
        {
            return coinsDelete;
        }

        internal static IEnumerable<Coin> GenereteListOfCoin()
        {
            return coins;
        }

        internal static IEnumerable<CoinDto> GenereteListOfCoinDtos()
        {
            return coinsDto;
        }

        internal static IEnumerable<RecivedCoinDto> GenereteListOfRecivedDtos()
        {
            return recivedCoinDtos;
        }

        internal static Coin GenerateCoin()
        {
            return coins[0];
        }

        internal static CoinDto GenerateCoinDtos()
        {
            return coinsDto[0];
        }

        internal static RecivedCoinDto GenerateRecivedDtos()
        {
            return recivedCoinDtos[0];
        }

        internal static CoinUpdate GenerateCoinUpdate()
        {
            return coinsUpdate[0];
        }

        internal static CoinInsert GenerateCoinInsert()
        {
            return coinsInsert[0];
        }

        internal static CoinDelete GenerateCoinDelete()
        {
            return coinsDelete[0];
        }

        private static List<CoinDelete> BuildCoinDeleteList()
        {
            var list = new List<CoinDelete>();

            foreach (var coin in genericCoins)
            {
                list.Add(new()
                {
                    Id = coin.Id,
                });
            }

            return list;
        }

        private static List<CoinUpdate> BuildCoinUpdateList()
        {
            var list = new List<CoinUpdate>();

            foreach (var coin in genericCoins)
            {
                list.Add(new(){
                    Id = coin.Id,
                    Symbol = coin.Symbol,
                    Name = coin.Name,
                    Value = coin.Value
                });
            }

            return list;
        }

        private static List<CoinInsert> BuildCoinInsertList()
        {
            var list = new List<CoinInsert>();

            foreach (var coin in genericCoins)
            {
                list.Add(new()
                {
                    Id = coin.Id,
                    Symbol = coin.Symbol,
                    Name = coin.Name,
                    Value = coin.Value
                });
            }

            return list;
        }
        private static List<Coin> BuildCoinList()
        {
            var list = new List<Coin>();

            foreach (var coin in genericCoins)
            {
                list.Add(new()
                {
                    Id = coin.Id,
                    Symbol = coin.Symbol,
                    Name = coin.Name,
                    Value = coin.Value
                });
            }

            return list;
        }

        private static List<CoinDto> BuildCoinDtosList()
        {
            var list = new List<CoinDto>();

            foreach (var coin in genericCoins)
            {
                list.Add(new()
                {
                    Id = coin.Id,
                    Symbol = coin.Symbol,
                    Name = coin.Name,
                    Value = coin.Value
                });
            }

            return list;
        }

        private static List<RecivedCoinDto> BuildRecivedDtosList()
        {
            var list = new List<RecivedCoinDto>();

            foreach (var coin in genericCoins)
            {
                list.Add(new()
                {
                    Symbol = coin.Symbol,
                    Name = coin.Name,
                    Value = coin.Value
                });
            }

            return list;
        }
    }
}
