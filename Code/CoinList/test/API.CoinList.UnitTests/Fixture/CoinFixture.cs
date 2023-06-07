using API.CoinList.Domain.Dtos;
using API.CoinList.Domain.Entities;
using API.Contracts.Coin;
using API.UnitTest.Utility.FixtureManager;

namespace API.CoinList.UnitTests.Fixture
{
    internal static class CoinFixture
    {
        private static FixureManger fixureManger = new FixureManger();

        internal static IEnumerable<CoinUpdate> GenereteListOfCoinUpdate()
        {
            return fixureManger.CreateList<CoinUpdate>();
        }

        internal static IEnumerable<CoinInsert> GenereteListOfCoinInsert()
        {
            return fixureManger.CreateList<CoinInsert>();
        }

        internal static IEnumerable<CoinDelete> GenereteListOfCoinDelete()
        {
            return fixureManger.CreateList<CoinDelete>();
        }

        internal static IEnumerable<Coin> GenereteListOfCoin()
        {
            return fixureManger.CreateList<Coin>();
        }

        internal static IEnumerable<CoinDto> GenereteListOfCoinDtos()
        {
            return fixureManger.CreateList<CoinDto>();
        }

        internal static IEnumerable<RecivedCoinDto> GenereteListOfRecivedDtos()
        {
            return fixureManger.CreateList<RecivedCoinDto>();
        }

        internal static Coin GenerateCoin()
        {
            return fixureManger.Create<Coin>();
        }

        internal static CoinDto GenerateCoinDtos()
        {
            return fixureManger.Create<CoinDto>();
        }

        internal static RecivedCoinDto GenerateRecivedDtos()
        {
            return fixureManger.Create<RecivedCoinDto>();
        }

        internal static CoinUpdate GenerateCoinUpdate()
        {
            return fixureManger.Create<CoinUpdate>();
        }

        internal static CoinInsert GenerateCoinInsert()
        {
            return fixureManger.Create<CoinInsert>();
        }

        internal static CoinDelete GenerateCoinDelete()
        {
            return fixureManger.Create<CoinDelete>();
        }

    }
}
