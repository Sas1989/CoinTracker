using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.SDK.UnitTests.Fixure
{
    public readonly record struct FakeRecivedDto(
        string stringProp,
        int intProp
    );
}
