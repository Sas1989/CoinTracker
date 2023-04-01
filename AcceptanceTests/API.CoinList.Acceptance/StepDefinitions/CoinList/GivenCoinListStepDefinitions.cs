using CoinTracker.API.CoinList.Acceptance.Support.CoinList;
using CoinTracker.API.CoinList.Acceptance.Support.CoinList.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.CoinList
{
    [Binding]
    public class GivenCoinListStepDefinitions : BaseStepDefinition
    {
        private CoinAction coinAction;
        public GivenCoinListStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient) : base(scenarioContext, httpClient)
        {
            this.coinAction = ActionFactory.GetAction<CoinAction>();
        }

        [Given(@"The following coin in the database")]
        public async Task GivenTheFollowingCoinInTheDatabaseAsync(Table table)
        {
            var coins = table.CreateSet<RecivedCoin>().ToList();
            var coinInDatabase = await coinAction.CreateMassive(coins);
            scenarioContext.Add(CoinKeys.DATABASE_COIN, coinInDatabase);
        }

        [Given(@"A coin (.*) with description (.*) and (.*) in the database")]
        public async Task GivenACoinBNBWithDescriptionBNBAndInTheDatabaseAsync(string symbol, string description, decimal value)
        {
            var sentCoin = await coinAction.Create(symbol, description, value);
            DatabaseCoin(sentCoin);
        }

        [Given(@"A coin not present in the database")]
        public void GivenANewIdNoPresentInTheApplication()
        {
            var coinNotInDatabase = new Coin { Id = Guid.NewGuid(), Symbol = "NotInDatabase", Name = "Not In the database", Value = 1 };
            DatabaseCoin(coinNotInDatabase);
        }

        [Given(@"A new coin (.*) with description (.*) and (.*) vaule is addeded")]
        public void GivenANewCoin(string symbol, string description, decimal value)
        {
            InputCoin(symbol, description, value);
        }

        [Given(@"A new coin (.*) with description (.*) with a negative value")]
        public void GivenACoinWithNegativeValue(string symbol, string description)
        {
            decimal negativevalue = -new Random().Next();
            InputCoin(symbol, description, negativevalue);
        }

        [Given(@"A new coins with the following value:")]
        public void GivenANewCoinWithTheFollowingValue(Table table)
        {
            var coins = table.CreateSet<RecivedCoin>();
            scenarioContext.Add(CoinKeys.INPUT_COIN, coins);
        }

        private void InputCoin(string symbol, string description, decimal value)
        {
            RecivedCoin coinToSend = new RecivedCoin { Symbol = symbol, Name = description, Value = value };
            scenarioContext.Add(CoinKeys.INPUT_COIN, coinToSend);
        }

        private void DatabaseCoin(Coin coin)
        {
            scenarioContext.Add(CoinKeys.DATABASE_COIN, coin);
            scenarioContext.Add(CoinKeys.DATABASE_COIN_ID, coin.Id);
            scenarioContext.Add(CoinKeys.DATABASE_COIN_SYMBOL, coin.Symbol);
        }

    }
}
