using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.UnitTests.Fixture
{
    internal static class CoinFixture
    {
        private static List<Coin> coins = BuildCoinList();
        private static List<CoinDto> coinsDto = BuildCoinDtosList();
        private static List<RecivedCoinDto> recivedCoinDtos = BuildRecivedDtosList();

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

        private static List<Coin> BuildCoinList()
        {
            return new List<Coin> {
                new Coin
                {
                    Id = Guid.Parse("1a049978-32a8-45b6-95d9-24b7847a534b"),
                    Symbol = "BTC",
                    Name =  "Bitcoin",
                    Value = 20000
                },
                new Coin
                {
                    Id = Guid.Parse("dd6dbd88-d005-45a3-98f0-aff222c45a07"),
                    Symbol = "ETH",
                    Name =  "Etherium",
                    Value = 1800
                }
            };
        }

        private static List<CoinDto> BuildCoinDtosList()
        {
            return new List<CoinDto> {
                new CoinDto
                {
                    Id = Guid.Parse("1a049978-32a8-45b6-95d9-24b7847a534b"),
                    Symbol = "BTC",
                    Name =  "Bitcoin",
                    Value = 20000
                },
                new CoinDto
                {
                    Id = Guid.Parse("dd6dbd88-d005-45a3-98f0-aff222c45a07"),
                    Symbol = "ETH",
                    Name =  "Etherium",
                    Value = 1800
                }
            };
        }

        private static List<RecivedCoinDto> BuildRecivedDtosList()
        {
            return new List<RecivedCoinDto> {
                new RecivedCoinDto
                {
                    Symbol = "BTC",
                    Name =  "Bitcoin",
                    Value = 20000
                },
                new RecivedCoinDto
                {
                    Symbol = "ETH",
                    Name =  "Etherium",
                    Value = 1800
                }
            };
        }
    }
}
