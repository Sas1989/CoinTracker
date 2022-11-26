using CoinTracker.API.CoinList.Acceptance.Support.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.InsertCoins
{
    [Binding]
    public class CoinListInsertFeatureStepsDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;

        public CoinListInsertFeatureStepsDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;

        }

        [Given(@"A new coin (.*) with description (.*) and (.*) vaule is addeded")]
        public void GivenANewCoin(string symbol, string description, decimal value)
        {
            CreateCoin(symbol, description, value);
        }

        [Given(@"A new coin (.*) with description (.*) with a negative value")]
        public void GivenACoinWithNegativeValue(string symbol, string description)
        {
            decimal negativevalue = -new Random().Next();
            CreateCoin(symbol, description, negativevalue);
        }

        [Given(@"A new coin with the following value:")]
        public void GivenANewCoinWithTheFollowingValue(Table table)
        {
            var coins = table.CreateSet<RecivedCoin>();
            scenarioContext.Add(CoinKeys.INPUT_COIN, coins);
        }

        private void CreateCoin(string symbol, string description, decimal value)
        {
            RecivedCoin coinToSend = new RecivedCoin { Symbol = symbol, Name = description, Value = value };
            scenarioContext.Add(CoinKeys.INPUT_COIN, coinToSend);
        }

        [When(@"Post the coin")]
        public async Task WhenPostTheCoinAsync()
        {
            await PostCoin<RecivedCoin>(CoinListEndPoint.API_COIN);
        }

        [When(@"Post the coins")]
        public async Task WhenYouPostTheCoinAsync()
        {
            await PostCoin<List<RecivedCoin>>(CoinListEndPoint.API_COIN_BULK);
        }

        private async Task PostCoin<T>(string endpoint)
        {
            var data = scenarioContext.Get<T>(CoinKeys.INPUT_COIN);
            var result = await httpClient.PostAsJsonAsync(endpoint, data);
            scenarioContext.Add(CoinKeys.ACTION_RESULT, result);
        }


        [Then(@"Recive the coin sent with a new id created")]
        public async Task ThenReciveTheCoinVauleWithANewIdCreatedAsync()
        {
            var result = scenarioContext.Get<HttpResponseMessage>(CoinKeys.ACTION_RESULT);
            var coin = await result.Content.ReadFromJsonAsync<Coin>();

            coin.Should().NotBeNull();
            coin.Id.Should().NotBeEmpty();

            scenarioContext.Add(CoinKeys.DATABASE_COIN_ID, coin.Id);

        }

        [Then(@"The coins are in the coin list")]
        public async Task ThenTheCoinsAreInTheCoinListAsync()
        {
            IEnumerable<Coin> coins = await CoinAction.GetCoins();
            List<RecivedCoin> sentCoins = scenarioContext.Get<List<RecivedCoin>>(CoinKeys.INPUT_COIN);

            List<RecivedCoin> recivedCoins = coins.Select(x => CoinAction.ToRecivedCoin(x)).ToList();

            foreach (RecivedCoin c in sentCoins)
            {
                recivedCoins.Should().ContainEquivalentOf(c);
            }
        }



    }
}
