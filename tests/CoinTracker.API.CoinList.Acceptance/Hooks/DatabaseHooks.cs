using CoinTracker.API.CoinList.Acceptance.Support.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Hooks
{
    [Binding]
    internal class DatabaseHooks
    {
        [BeforeScenario]
        public void EmptyDatabase()
        {
            CoinAction.Clean();
        }
    }
}
