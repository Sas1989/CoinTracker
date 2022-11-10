using CoinTracker.API.CoinList.Acceptance.StepDefinitions.CommonSteps;
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

        [When(@"Put a new value (.*) using his Id")]
        public async Task WhenPostANewValueByIdAsync(decimal newValue)
        {
            var newValueCoin = SetCoin(value: newValue);

            await PutCoinByID(newValueCoin);
        }

        [When(@"Put a new value (.*) using his Symbol")]
        public async Task WhenPostANewValueBySymbolAsync(decimal newValue)
        {
            var newValueCoin = SetCoin(value: newValue);

            await PutCoinBySymbol(newValueCoin);
        }

        [When(@"Put a new symbol (.*) and (.*)")]
        public async Task WhenPostANewSymbolLuncAndTerraClassicAsync(string symbol, string description)
        {
            var newValueCoin = SetCoin(symbol: symbol, name: description);

            await PutCoinByID(newValueCoin);

        }

        private RecivedCoin SetCoin(string? symbol = null, string? name = null, decimal? value = null)
        {
            var newValueCoin = scenarioContext.Get<Coin>(CoinKeys.DATABASE_COIN);
            
            if(symbol is not null) newValueCoin.Symbol = symbol;
            if(name is not null) newValueCoin.Name = name;
            if(value is not null) newValueCoin.Value = (decimal)value;
            
            return CoinAction.ToRecivedCoin(newValueCoin);
        }

        private async Task PutCoinByID(RecivedCoin newCoin)
        {
            var id = scenarioContext.Get<Guid>(CoinKeys.DATABASE_COIN_ID);
            string endPoint = $"{CoinListEndPoint.API_COIN}/{id}";

            await PutCoin(endPoint, newCoin);
        }

        private async Task PutCoinBySymbol(RecivedCoin newCoin)
        {
            string endPoint = $"{CoinListEndPoint.API_COIN}/symbol/{newCoin.Symbol}";
            await PutCoin(endPoint, newCoin);
        }


        private async Task PutCoin(string endPoint, RecivedCoin newCoin)
        {
            var result = await httpClient.PutAsJsonAsync(endPoint, newCoin);

            scenarioContext.Add(CoinKeys.ACTION_RESULT, result);
            scenarioContext.Add(CoinKeys.INPUT_COIN, newCoin);
        }

    }
}
