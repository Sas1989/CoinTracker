using CoinTracker.API.CoinList.Acceptance.Support.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.CommonSteps
{
    [Binding]
    public class ThenCoinCommonSteps
    {
        public readonly ScenarioContext scenarioContext;
        public readonly HttpClient httpClient;

        public ThenCoinCommonSteps(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
        }

        [Then(@"The coin is created|updated sucessfully")]
        public async Task ThenThecoinIsCreatedSucessfulltAsync()
        {
            var id = scenarioContext.Get<Guid>(CoinKeys.DATABASE_COIN_ID);
            var expectedCoin = scenarioContext.Get<RecivedCoin>(CoinKeys.INPUT_COIN);
            var result = scenarioContext.Get<HttpResponseMessage>(CoinKeys.ACTION_RESULT);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var actualCoin = await CoinAction.GetCoin(id);

            actualCoin.Id.Should().Be(id);
            actualCoin.Symbol.Should().Be(expectedCoin.Symbol);
            actualCoin.Name.Should().Be(expectedCoin.Name);
            actualCoin.Value.Should().Be(expectedCoin.Value);

        }

        [Then(@"Recive en error message")]
        public void ThenIReciveEnErrorMessage()
        {
            var result = scenarioContext.Get<HttpResponseMessage>(CoinKeys.ACTION_RESULT);
            result.StatusCode.Should().NotBe(System.Net.HttpStatusCode.OK);
        }

        [Then(@"Recive a (.*) status")]
        public void ThenReciveAStatus(int statusCode)
        {
            var result = scenarioContext.Get<HttpResponseMessage>(CoinKeys.ACTION_RESULT);
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)statusCode);
        }

        [Then(@"I recive the requested coin")]
        public async Task ThenIReciveTheRequestedCoinAsync()
        {
            var result = scenarioContext.Get<HttpResponseMessage>(CoinKeys.ACTION_RESULT);
            Coin coin = await result.Content.ReadFromJsonAsync<Coin>();
            Coin expectedCoin = scenarioContext.Get<Coin>(CoinKeys.DATABASE_COIN);
            coin.Should().Be(expectedCoin);
        }
    }
}
