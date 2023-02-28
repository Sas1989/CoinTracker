using CoinTracker.API.CoinList.Acceptance.Support.CoinList;
using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions;
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
        [BeforeScenario,AfterScenario]
        public async Task EmptyDatabaseAsync()
        {
            var actions = ActionFactory.GetAll();
            foreach(var action in actions)
            {
                await action.Clean();
            }
        }
    }
}
