using CoinTracker.API.CoinList.Acceptance.Support.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.GetCoins
{
    [Binding]
    public class CoinCommonSteps
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;

        public CoinCommonSteps(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
        }

        [Given(@"A coin (.*) with description (.*) and (.*) in the database")]
        public async Task GivenACoinBNBWithDescriptionBNBAndInTheDatabaseAsync(string symbol, string description, decimal value)
        {
            var id = await CoinAction.CreateNewCoinAsync(symbol, description, value);
            Coin sentCoin = new Coin { Id = id, Symbol = symbol, Name = description, Value = value };
            scenarioContext.Add("SentCoin", sentCoin);
            scenarioContext.Add("CoinId", id);
            scenarioContext.Add("CoinSymbol", symbol);
        }

        [Then(@"I recive the requested coin")]
        public async Task ThenIReciveTheRequestedCoinAsync()
        {
            var result = scenarioContext.Get<HttpResponseMessage>("result");
            Coin coin = await result.Content.ReadFromJsonAsync<Coin>();
            Coin expectedCoin = scenarioContext.Get<Coin>("SentCoin");
            coin.Should().Be(expectedCoin);
        }

        [Then(@"I recive a 404 status")]
        public void ThenIReciveAStatus()
        {
            var result = scenarioContext.Get<HttpResponseMessage>("result");
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }
    }
}
