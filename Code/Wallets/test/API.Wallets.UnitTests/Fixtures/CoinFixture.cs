using API.Contracts.Coin;
using API.UnitTest.Utility.FixtureManager;
using API.Wallets.Domain.Dtos.Coin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.UnitTests.Fixtures
{
    internal static class CoinFixture
    {
        private static FixureManger fixtureManager = new FixureManger();

        internal static CoinInsert CoinInsert()
        {
            return fixtureManager.Create<CoinInsert>();
        }

        internal static CoinUpdate CoinUpdate()
        {
            return fixtureManager.Create<CoinUpdate>();
        }

        internal static CoinDto CoinDto()
        {
            return fixtureManager.Create<CoinDto>();
        }

        internal static CoinDelete CoinDelete()
        {
            return fixtureManager.Create<CoinDelete>();
        }
    }
}
