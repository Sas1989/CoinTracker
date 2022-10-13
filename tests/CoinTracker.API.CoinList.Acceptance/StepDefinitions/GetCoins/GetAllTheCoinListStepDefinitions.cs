using CoinTracker.API.CoinList.Acceptance.Support.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services;
using System;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.GetCoin
{
    [Binding]
    public class GetAllTheCoinListStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;

        public GetAllTheCoinListStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
        }

        [Given(@"The following coin in the database")]
        public async Task GivenTheFollowingCoinInTheDatabaseAsync(Table table)
        {
            var coinInDatabase = table.CreateSet<RecivedCoin>().ToList();
            await CoinAction.CreateMassiveCoinAsync(coinInDatabase);
            scenarioContext.Add("coinInDatabase", coinInDatabase);
        }


        [When(@"Request All the coin")]
        public async Task WhenRequestAllTheCoin()
        {
            var result = await httpClient.GetAsync(CoinListEndPoint.API_COIN);
            scenarioContext.Add("result", result);
        }

        [Then(@"I recive all the coin")]
        public async Task ThenIReciveAllTheCoinAsync()
        {
            var result = scenarioContext.Get<HttpResponseMessage>("result");
            List<Coin> coin = await result.Content.ReadFromJsonAsync<List<Coin>>();
            List<RecivedCoin> sentCoins = scenarioContext.Get<List<RecivedCoin>>("coinInDatabase");
            List<RecivedCoin> recivedCoins = coin.Select(x => new RecivedCoin { Name = x.Name, Symbol = x.Symbol, Value = x.Value }).ToList();
            
            foreach (RecivedCoin c in sentCoins)
            {
                recivedCoins.Should().ContainEquivalentOf(c);
            }
        }



    }
}
