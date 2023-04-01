using CoinTracker.API.CoinList.Acceptance.Support.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.ActionStep
{
    [Binding]
    public class ResultStepDefinition : BaseStepDefinition
    {
        public ResultStepDefinition(ScenarioContext scenarioContext, HttpClient httpClient) : base(scenarioContext, httpClient)
        {
        }

        [Then(@"Recive a (.*) status")]
        public void ThenReciveAStatus(int statusCode)
        {
            var result = scenarioContext.Get<HttpResponseMessage>(ActionKeys.ACTION_RESULT);
            result.StatusCode.Should().Be((HttpStatusCode)statusCode);
        }

        [Then(@"Recive en error message")]
        public void ThenIReciveEnErrorMessage()
        {
            var result = scenarioContext.Get<HttpResponseMessage>(ActionKeys.ACTION_RESULT);
            result.StatusCode.Should().NotBe(HttpStatusCode.OK);
        }
    }
}
