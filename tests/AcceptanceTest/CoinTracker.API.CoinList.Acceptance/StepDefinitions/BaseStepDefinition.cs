using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions;
using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions
{
    public class BaseStepDefinition
    {
        protected readonly ScenarioContext scenarioContext;
        protected readonly HttpClient httpClient;
        protected readonly ICleanable action;

        public BaseStepDefinition(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
        }

    }
}
