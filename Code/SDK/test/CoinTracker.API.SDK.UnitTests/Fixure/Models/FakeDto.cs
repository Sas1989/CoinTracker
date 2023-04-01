using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.SDK.UnitTests.Fixure
{
    public readonly record struct FakeDto(
        Guid guidProp,
        string stringProp,
        int intProp
    );
}
