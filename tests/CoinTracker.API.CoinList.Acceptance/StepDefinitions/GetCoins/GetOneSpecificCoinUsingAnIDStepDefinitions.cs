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
    public class GetOneSpecificCoinUsingAnIDStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;

        public GetOneSpecificCoinUsingAnIDStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
        }


        [Given(@"A new Id no present in the application")]
        public void GivenANewIdNoPresentInTheApplication()
        {
            scenarioContext.Add("CoinId", Guid.NewGuid());
        }

        [When(@"Request coin using his ID")]
        public async Task WhenRequestCoinUsingHisIDAsync()
        {
            var id = scenarioContext.Get<Guid>("CoinId");
            var result = await httpClient.GetAsync($"{CoinListEndPoint.API_COIN}/{id}");
            scenarioContext.Add("result", result);
        }

    }
}
