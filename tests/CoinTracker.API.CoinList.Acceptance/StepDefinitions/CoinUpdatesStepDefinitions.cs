using CoinTracker.API.CoinList.Acceptance.Support.Services;
using System;
using TechTalk.SpecFlow;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions
{
    [Binding]
    public class CoinUpdatesStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient httpClient;

        public CoinUpdatesStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient)
        {
            this.scenarioContext = scenarioContext;
            this.httpClient = httpClient;
        }

        [Given(@"The (.*) (.*) in the database with value (.*)")]
        public void GivenTheBTCBitcoinInTheDatabaseWithValue(string symbol, string name, decimal value)
        {
            throw new PendingStepException();
        }

        [When(@"Post a new value (.*)")]
        public void WhenPostANewValue(int newValue)
        {
            throw new PendingStepException();
        }

        [Then(@"The value is changed sucessfully")]
        public void ThenTheValueIsChangedSucessfully()
        {
            throw new PendingStepException();
        }

        [When(@"Post a new symbol Lunc and TerraClassic")]
        public void WhenPostANewSymbolLuncAndTerraClassic()
        {
            throw new PendingStepException();
        }

        [Then(@"The symbol and the description are changed")]
        public void ThenTheSymbolAndTheDescriptionAreChanged()
        {
            throw new PendingStepException();
        }

    }
}
