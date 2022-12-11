using CoinTracker.API.CoinList.Acceptance.Support.CoinList;
using CoinTracker.API.CoinList.Acceptance.Support.CoinList.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions;
using System;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.CoinList.GetCoins
{
    [Binding]
    public class GetCoinListStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;
        private readonly CoinAction coinAction;

        public GetCoinListStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
            this.coinAction = ActionFactory.GetAction<CoinAction>();
        }

        [Given(@"The following coin in the database")]
        public async Task GivenTheFollowingCoinInTheDatabaseAsync(Table table)
        {
            var coinInDatabase = table.CreateSet<RecivedCoin>().ToList();
            await coinAction.CreateMassive(coinInDatabase);
            scenarioContext.Add(CoinKeys.INPUT_COIN, coinInDatabase);
        }


        [When(@"Request All the coin")]
        public async Task WhenRequestAllTheCoin()
        {
            var result = await httpClient.GetAsync(CoinListEndPoint.API_COIN);
            scenarioContext.Add(CoinKeys.ACTION_RESULT, result);
        }

        [Then(@"I recive all the coin")]
        public async Task ThenIReciveAllTheCoinAsync()
        {
            var result = scenarioContext.Get<HttpResponseMessage>(CoinKeys.ACTION_RESULT);
            IEnumerable<Coin> coins = await result.Content.ReadFromJsonAsync<List<Coin>>();

            List<RecivedCoin> sentCoins = scenarioContext.Get<List<RecivedCoin>>(CoinKeys.INPUT_COIN);

            List<RecivedCoin> recivedCoins = coins.Select(x => (RecivedCoin)x).ToList();

            foreach (RecivedCoin c in sentCoins)
            {
                recivedCoins.Should().ContainEquivalentOf(c);
            }
        }



    }
}
