using CoinTracker.API.CoinList.Acceptance.StepDefinitions.CommonSteps;
using CoinTracker.API.CoinList.Acceptance.Support.Services;
using System;
using TechTalk.SpecFlow;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions
{
    [Binding]
    public class GetCoinUsingSymbolStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;

        public GetCoinUsingSymbolStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
        }

        [Given(@"A coin (.*) not presen in the database")]
        public void GivenACoinSASNotPresenInTheDatabase(string Symbol)
        {
            scenarioContext.Add(CoinKeys.DATABASE_COIN_SYMBOL, Symbol);
        }

        [When(@"Request coin using his Symbol")]
        public async Task WhenRequestCoinUsingHisSymbolAsync()
        {
            var symbol = scenarioContext.Get<string>(CoinKeys.DATABASE_COIN_SYMBOL);
            var result = await httpClient.GetAsync($"{CoinListEndPoint.API_COIN}/symbol/{symbol}");
            scenarioContext.Add(CoinKeys.ACTION_RESULT, result);
        }


    }
}
