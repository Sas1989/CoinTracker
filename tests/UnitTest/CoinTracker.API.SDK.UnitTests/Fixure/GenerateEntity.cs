using CoinTracker.API.UnitTest.Utiltiy.FixtureManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.SDK.UnitTests.Fixure
{
    internal static class GenerateEntity
    {
        private static FixureManger fixureManger = new FixureManger();
        public static FakeEntity Generate()
        {
            return fixureManger.Create<FakeEntity>();
        }

        public static IEnumerable<FakeEntity> GenerateList()
        {
            return fixureManger.CreateList<FakeEntity>();
        }
    }
}
