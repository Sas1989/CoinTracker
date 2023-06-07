using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SDK.UnitTests.Fixure.Models
{
    public readonly record struct FakeRecivedDto(
        string stringProp,
        int intProp
    );
}
