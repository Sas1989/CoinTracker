using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.SDK.UnitTests.Fixure
{
    internal static class GenerateEntity
    {
        public static FakeEntity Generate()
        {
            return new FakeEntity { Id = Guid.NewGuid() };
        }

        public static IEnumerable<FakeEntity> GenerateList()
        {
            var list = new List<FakeEntity>();
            for(int i=0; i < 10; i++)
            {
                list.Add(new FakeEntity { Id = Guid.NewGuid() });
            }

            return list;
        }
    }
}
