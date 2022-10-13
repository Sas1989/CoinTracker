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

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions
{
    [Binding]
    public class CoinListInsertFeatureStepsDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HttpClient httpClient;

        public CoinListInsertFeatureStepsDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            _scenarioContext = scenarioContext;
            this.httpClient = httpClient;

        }

        [Given(@"A new coin (.*) with description (.*) and (.*) vaule is addeded")]
        public void GivenANewCoin(string symbol, string description, decimal value)
        {
            CreateCoin(symbol, description, value);
        }

        [When(@"Post the coin")]
        public async Task WhenPostTheCoinAsync()
        {
            var data = _scenarioContext.Get<RecivedCoin>("CoinToSend");
            var result = await httpClient.PostAsJsonAsync(CoinListEndPoint.API_COIN, data);
            _scenarioContext.Add("Result", result);
        }

        [Then(@"The new coins? is|are created sucessfully")]
        public void ThenTheNewCoinIsCreatedInTheDatabase()
        {
            var result = _scenarioContext.Get<HttpResponseMessage>("Result");
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }

        [Then(@"Recive the coin sent with a new id created")]
        public async Task ThenReciveTheCoinVauleWithANewIdCreatedAsync()
        {
            var result = _scenarioContext.Get<HttpResponseMessage>("Result");
            var coinSented = _scenarioContext.Get<RecivedCoin>("CoinToSend");
            var coin = await result.Content.ReadFromJsonAsync<Coin>();
            
            using(new AssertionScope())
            {
                coin.Should().NotBeNull();
                coin.Id.Should().NotBeEmpty();
                coin.Name.Should().Be(coinSented.Name);
                coin.Symbol.Should().Be(coinSented.Symbol);
                coin.Value.Should().Be(coinSented.Value);
            }

        }

        [Given(@"A new coin (.*) with description (.*) with a negative value")]
        public void GivenACoinWithNegativeValue(string symbol, string description)
        {
            decimal negativevalue = -new Random().Next();
            CreateCoin(symbol, description, negativevalue);
        }

        [Then(@"Recive en error message")]
        public void ThenIReciveEnErrorMessage()
        {
            var result = _scenarioContext.Get<HttpResponseMessage>("Result");
            result.StatusCode.Should().NotBe(System.Net.HttpStatusCode.OK);
        }


        [Given(@"A new coin with the following value:")]
        public void GivenANewCoinWithTheFollowingValue(Table table)
        {
            var coins = table.CreateSet<RecivedCoin>();
            _scenarioContext.Add("ListCoinsToSend", coins);
        }

        [When(@"Post the coins")]
        public async Task WhenYouPostTheCoinAsync()
        {
            var data = _scenarioContext.Get<List<RecivedCoin>>("ListCoinsToSend");
            var result = await httpClient.PostAsJsonAsync(CoinListEndPoint.API_COIN_BULK, data);
            _scenarioContext.Add("Result", result);
        }

        [Then(@"The coins are in the coin list")]
        public async Task ThenTheCoinsAreInTheCoinListAsync()
        {
            var result = _scenarioContext.Get<HttpResponseMessage>("Result");
            List<Coin> coin = await result.Content.ReadFromJsonAsync<List<Coin>>();
            List<RecivedCoin> sentCoins = _scenarioContext.Get<List<RecivedCoin>>("ListCoinsToSend");

            List<RecivedCoin> recivedCoins = coin.Select(x => new RecivedCoin { Name = x.Name, Symbol = x.Symbol, Value = x.Value }).ToList();

            foreach(RecivedCoin c in sentCoins)
            {
                recivedCoins.Should().ContainEquivalentOf(c);
            }
        }

        private void CreateCoin(string symbol, string description, decimal value)
        {
            RecivedCoin coinToSend = new RecivedCoin { Symbol = symbol, Name = description, Value = value };
            _scenarioContext.Add("CoinToSend", coinToSend);
        }

    }
}
