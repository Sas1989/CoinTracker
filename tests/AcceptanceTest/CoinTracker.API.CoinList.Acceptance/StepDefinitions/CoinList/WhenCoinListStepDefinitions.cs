using CoinTracker.API.CoinList.Acceptance.Support.Action;
using CoinTracker.API.CoinList.Acceptance.Support.CoinList;
using CoinTracker.API.CoinList.Acceptance.Support.CoinList.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.CoinList
{
    [Binding]
    public class WhenCoinListStepDefinitions : BaseStepDefinition
    {
        private CoinAction coinAction;
        public WhenCoinListStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient) : base(scenarioContext, httpClient)
        {
            this.coinAction = ActionFactory.GetAction<CoinAction>();
        }

        [When(@"Get coin using ID")]
        public async Task WhenGetCoinUsingIDAsync()
        {
            var id = scenarioContext.Get<Guid>(CoinKeys.DATABASE_COIN_ID);
            await GetCoin(id);
        }

        [When(@"Get All coins")]
        public async Task WhenRequestAllTheCoin()
        {
            await GetCoin();
        }

        [When(@"Request coin using his Symbol")]
        public async Task WhenRequestCoinUsingHisSymbolAsync()
        {
            var symbol = scenarioContext.Get<string>(CoinKeys.DATABASE_COIN_SYMBOL);
            await GetCoin(symbol);

        }

        [When(@"Delete coin using ID")]
        public async Task WhenDeleteCoinUsingIDAsync()
        {
            var id = scenarioContext.Get<Guid>(CoinKeys.DATABASE_COIN_ID);
            await DeleteCoin(id);
        }

        [When(@"Post the coin")]
        public async Task WhenPostTheCoinAsync()
        {
            await PostCoin(CoinListEndPoint.API_COIN);

        }

        [When(@"Post the coins")]
        public async Task WhenYouPostTheCoinAsync()
        {
            await PostCoin(CoinListEndPoint.API_COIN_BULK);
        }

        [When(@"Put a new value (.*) using his Id")]
        public async Task WhenPostANewValueByIdAsync(decimal newValue)
        {
            SetCoin(value: newValue);

            await PutCoinByID();
        }

        [When(@"Put a new value (.*) using his Symbol")]
        public async Task WhenPostANewValueBySymbolAsync(decimal newValue)
        {
            SetCoin(value: newValue);

            await PutCoinBySymbol();
        }

        [When(@"Put a new symbol (.*) and (.*)")]
        public async Task WhenPostANewSymbolLuncAndTerraClassicAsync(string symbol, string description)
        {
            SetCoin(symbol: symbol, name: description);

            await PutCoinByID();

        }

        private async Task GetCoin(string symbol)
        {
            var result = await httpClient.GetAsync($"{CoinListEndPoint.API_COIN}/symbol/{symbol}");
            scenarioContext.Add(ActionKeys.ACTION_RESULT, result);
        }

        private async Task GetCoin(Guid id)
        {
            var result = await httpClient.GetAsync($"{CoinListEndPoint.API_COIN}/{id}");
            scenarioContext.Add(ActionKeys.ACTION_RESULT, result);
        }

        private async Task GetCoin()
        {
            var result = await httpClient.GetAsync(CoinListEndPoint.API_COIN);
            scenarioContext.Add(ActionKeys.ACTION_RESULT, result);
        }

        private async Task DeleteCoin(Guid id)
        {
            var result = await httpClient.DeleteAsync($"{CoinListEndPoint.API_COIN}/{id}");
            scenarioContext.Add(ActionKeys.ACTION_RESULT, result);
        }

        private async Task PostCoin(string endpoint)
        {
            var data = scenarioContext.Get<object>(CoinKeys.INPUT_COIN);
            var result = await httpClient.PostAsJsonAsync(endpoint, data);
            scenarioContext.Add(ActionKeys.ACTION_RESULT, result);
        }

        private void SetCoin(string? symbol = null, string? name = null, decimal? value = null)
        {
            var newValueCoin = scenarioContext.Get<Coin>(CoinKeys.DATABASE_COIN);

            if (symbol is not null) newValueCoin.Symbol = symbol;
            if (name is not null) newValueCoin.Name = name;
            if (value is not null) newValueCoin.Value = (decimal)value;

            scenarioContext.Add(CoinKeys.INPUT_COIN, (RecivedCoin)newValueCoin);

        }

        private async Task PutCoinByID()
        {
            var id = scenarioContext.Get<Guid>(CoinKeys.DATABASE_COIN_ID);
            string endPoint = $"{CoinListEndPoint.API_COIN}/{id}";

            await PutCoin(endPoint);
        }

        private async Task PutCoinBySymbol()
        {
            var symbol = scenarioContext.Get<string>(CoinKeys.DATABASE_COIN_SYMBOL);
            string endPoint = $"{CoinListEndPoint.API_COIN}/symbol/{symbol}";
            await PutCoin(endPoint);
        }


        private async Task PutCoin(string endPoint)
        {
            var data = scenarioContext.Get<RecivedCoin>(CoinKeys.INPUT_COIN);
            var result = await httpClient.PutAsJsonAsync(endPoint, data);

            scenarioContext.Add(ActionKeys.ACTION_RESULT, result);
        }
    }
}
