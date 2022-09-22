using CoinTracker.API.CoinList.Acceptance.Support.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services;
using System;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions
{
    [Binding]
    public class CoinListGetFeatureStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;

        public CoinListGetFeatureStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
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

        [Given(@"A coin (.*) with description (.*) and (.*) in the database")]
        public async Task GivenACoinBNBWithDescriptionBNBAndInTheDatabaseAsync(string symbol, string description, decimal value)
        {
            var id = await CoinAction.CreateNewCoinAsync(symbol, description, value);
            Coin expectedCoin = new Coin { Id = id, Symbol = symbol, Name = description, Value = value };
            scenarioContext.Add("expectedCoin", expectedCoin);
            scenarioContext.Add("CoinId",id);

        }

        [When(@"Request coin using his ID")]
        public async Task WhenRequestCoinUsingHisIDAsync()
        {
            var id = scenarioContext.Get<Guid>("CoinId");
            var result = await httpClient.GetAsync($"{CoinListEndPoint.API_COIN}/{id}");
            scenarioContext.Add("result", result);
        }

        [When(@"Request coin using his Symbol")]
        public void WhenRequestCoinUsingHisSymbol()
        {
            throw new PendingStepException();
        }


        [Then(@"I recive the requested coin")]
        public async Task ThenIReciveTheRequestedCoinAsync()
        {
            var result = scenarioContext.Get<HttpResponseMessage>("result");
            Coin coin = await result.Content.ReadFromJsonAsync<Coin>();
            Coin expectedCoin = scenarioContext.Get<Coin>("expectedCoin");
            coin.Should().Equals(expectedCoin);
        }


        [Given(@"A coin SAS not presen in the database")]
        public void GivenACoinSASNotPresenInTheDatabase()
        {
            throw new PendingStepException();
        }

        [Then(@"I recive a (.*) error")]
        public void ThenIReciveAError(int p0)
        {
            throw new PendingStepException();
        }

        [Given(@"A coin Luna Terra with value (.*)")]
        public void GivenACoinLunaTerraWithValue(int p0)
        {
            throw new PendingStepException();
        }

        [Given(@"Change his symbol to Lunc")]
        public void GivenChangeHisSymbolToLunc()
        {
            throw new PendingStepException();
        }

        [When(@"Request coin using his new Symbol")]
        public void WhenRequestCoinUsingHisNewSymbol()
        {
            throw new PendingStepException();
        }


    }
}
