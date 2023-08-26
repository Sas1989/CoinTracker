using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.UnitTest.Utility.FixtureManager
{
    public static class FixureManger
    {
        private static readonly Fixture fixture = new();
        public static T Create<T>()
        {
            return fixture.Create<T>();
        }

        public static IEnumerable<T> CreateList<T>()
        {
            return fixture.CreateMany<T>();
        }
    }
}
