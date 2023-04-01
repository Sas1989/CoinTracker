using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.UnitTest.Utiltiy.FixtureManager
{
    public class FixureManger
    {
        private readonly Fixture fixture;

        public FixureManger()
        {
            fixture = new Fixture();
        }

        public T Create<T>()
        {
            return fixture.Create<T>();
        }

        public IEnumerable<T> CreateList<T>()
        {
            return fixture.CreateMany<T>();
        }
    }
}
