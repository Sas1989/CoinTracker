using CoinTracker.API.CoinList.Acceptance.StepDefinitions.CommonSteps;
using CoinTracker.API.CoinList.Acceptance.Support.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services;
using System;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.GetCoin
{
    [Binding]
    public class GetCoinListStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;

        public GetCoinListStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
        }

        [Given(@"The following coin in the database")]
        public async Task GivenTheFollowingCoinInTheDatabaseAsync(Table table)
        {
            var coinInDatabase = table.CreateSet<RecivedCoin>().ToList();
            await CoinAction.CreateMassiveCoinAsync(coinInDatabase);
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

            List<RecivedCoin> recivedCoins = coins.Select(x => CoinAction.ToRecivedCoin(x)).ToList();

            foreach (RecivedCoin c in sentCoins)
            {
                recivedCoins.Should().ContainEquivalentOf(c);
            }
        }



    }
}
