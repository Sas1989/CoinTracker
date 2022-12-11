using CoinTracker.API.CoinList.Acceptance.Support.CoinList;
using CoinTracker.API.CoinList.Acceptance.Support.CoinList.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.CoinList.CommonSteps
{
    [Binding]
    public class GivenCoinCommonSteps
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;
        private readonly CoinAction coinAction;

        public GivenCoinCommonSteps(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
            this.coinAction = ActionFactory.GetAction<CoinAction>();
        }

        [Given(@"A coin (.*) with description (.*) and (.*) in the database")]
        public async Task GivenACoinBNBWithDescriptionBNBAndInTheDatabaseAsync(string symbol, string description, decimal value)
        {
            Coin sentCoin = await coinAction.Create(symbol, description, value);
            scenarioContext.Add(CoinKeys.DATABASE_COIN, sentCoin);
            scenarioContext.Add(CoinKeys.DATABASE_COIN_ID, sentCoin.Id);
            scenarioContext.Add(CoinKeys.DATABASE_COIN_SYMBOL, sentCoin.Symbol);
        }

        [Given(@"A new Id no present in the application with random data")]
        public void GivenANewIdNoPresentInTheApplication()
        {
            scenarioContext.Add(CoinKeys.DATABASE_COIN_ID, Guid.NewGuid());
            scenarioContext.Add(CoinKeys.DATABASE_COIN, new Coin { Symbol = "NotInDatabase", Name = "Not In the database", Value = 1 });
            scenarioContext.Add(CoinKeys.DATABASE_COIN_SYMBOL, string.Empty);
        }
    }
}
