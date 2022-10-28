using CoinTracker.API.CoinList.Acceptance.Support.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services;
using System;
using System.Net;
using System.Net.Http.Json;
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
        public async Task GivenTheBTCBitcoinInTheDatabaseWithValueAsync(string symbol, string name, decimal value)
        {
            var sendCoin = new RecivedCoin { Name = name, Symbol = symbol, Value = value };
            var id = await CoinAction.CreateNewCoinAsync(sendCoin);
            scenarioContext.Add("CoinIdInDatabase", id);
            scenarioContext.Add("CoinInDatabase", sendCoin);
        }

        [When(@"Put a new value (.*) using his Id")]
        public async Task WhenPostANewValueByIdAsync(decimal newValue)
        {
            var id = scenarioContext.Get<Guid>("CoinIdInDatabase");
            var newValueCoin = scenarioContext.Get<RecivedCoin>("CoinInDatabase");
            newValueCoin.Value = newValue;

            var result = await httpClient.PutAsJsonAsync($"{CoinListEndPoint.API_COIN}/{id}", newValueCoin);
            scenarioContext.Add("PutResult",result);
            scenarioContext.Add("NewCoin", newValueCoin);
        }

        [When(@"Put a new value (.*) using his Symbol")]
        public async Task WhenPostANewValueBySymbolAsync(decimal newValue)
        {
            var id = scenarioContext.Get<Guid>("CoinIdInDatabase");
            var newValueCoin = scenarioContext.Get<RecivedCoin>("CoinInDatabase");
            newValueCoin.Value = newValue;

            var result = await httpClient.PutAsJsonAsync($"{CoinListEndPoint.API_COIN}/symbol/{newValueCoin.Symbol}", newValueCoin);
            scenarioContext.Add("PutResult", result);
            scenarioContext.Add("NewCoin", newValueCoin);
        }

        [When(@"Put a new symbol (.*) and (.*)")]
        public async Task WhenPostANewSymbolLuncAndTerraClassicAsync(string symbol, string description)
        {
            var id = scenarioContext.Get<Guid>("CoinIdInDatabase");
            var newValueCoin = scenarioContext.Get<RecivedCoin>("CoinInDatabase");
            newValueCoin.Symbol = symbol;
            newValueCoin.Name = description;


            var result = await httpClient.PutAsJsonAsync($"{CoinListEndPoint.API_COIN}/{id}", newValueCoin);
            scenarioContext.Add("PutResult", result);
            scenarioContext.Add("NewCoin", newValueCoin);

        }

        [Then(@"The value is changed sucessfully")]
        public async Task ThenTheValueIsChangedSucessfullyAsync()
        {
            var id = scenarioContext.Get<Guid>("CoinIdInDatabase");
            var expectedCoin = scenarioContext.Get<RecivedCoin>("NewCoin");

            var result = scenarioContext.Get<HttpResponseMessage>("PutResult");
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var actualCoin = await CoinAction.GetCoin(id);

            actualCoin.Value.Should().Be(expectedCoin.Value);
        }


        [Then(@"The symbol and the description are changed")]
        public async Task ThenTheSymbolAndTheDescriptionAreChangedAsync()
        {
            var id = scenarioContext.Get<Guid>("CoinIdInDatabase");
            var expectedCoin = scenarioContext.Get<RecivedCoin>("NewCoin");

            var result = scenarioContext.Get<HttpResponseMessage>("PutResult");
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var actualCoin = await CoinAction.GetCoin(id);

            actualCoin.Symbol.Should().Be(expectedCoin.Symbol);
            actualCoin.Name.Should().Be(expectedCoin.Name);

        }

    }
}
