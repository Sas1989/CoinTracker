using CoinTracker.API.CoinList.Acceptance.Support.CoinList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.CoinList.GetCoins
{
    [Binding]
    public class GetCoinByIdStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;

        public GetCoinByIdStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
        }


        [When(@"Request coin using his ID")]
        public async Task WhenRequestCoinUsingHisIDAsync()
        {
            var id = scenarioContext.Get<Guid>(CoinKeys.DATABASE_COIN_ID);
            var result = await httpClient.GetAsync($"{CoinListEndPoint.API_COIN}/{id}");
            scenarioContext.Add(CoinKeys.ACTION_RESULT, result);
        }

    }
}
