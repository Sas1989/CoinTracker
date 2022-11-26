using CoinTracker.API.CoinList.Acceptance.Support.Services;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.DeleteCoins
{
    [Binding]
    public class DeleteCoinByIdStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;

        public DeleteCoinByIdStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
        }

        [When(@"Delete coin using ID")]
        public async Task WhenDeleteCoinUsingIDAsync()
        {
            var id = scenarioContext.Get<Guid>(CoinKeys.DATABASE_COIN_ID);
            var result = await httpClient.DeleteAsync($"{CoinListEndPoint.API_COIN}/{id}");
            scenarioContext.Add(CoinKeys.ACTION_RESULT, result);
        }

        [Then(@"Coin is deleted")]
        public async Task ThenCoinIsDeletedAsync()
        {
            var id = scenarioContext.Get<Guid>(CoinKeys.DATABASE_COIN_ID);
            var result = await httpClient.GetAsync($"{CoinListEndPoint.API_COIN}/{id}");

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
