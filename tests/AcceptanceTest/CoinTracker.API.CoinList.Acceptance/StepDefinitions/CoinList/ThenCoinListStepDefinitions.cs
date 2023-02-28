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
    public class ThenCoinListStepDefinitions : BaseStepDefinition
    {
        private CoinAction coinAction;
        public ThenCoinListStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient) : base(scenarioContext, httpClient)
        {
            this.coinAction = ActionFactory.GetAction<CoinAction>();
        }

        [Then(@"The coin is saved sucessfully")]
        public async Task ThenThecoinIsCreatedSucessfulltAsync()
        {
            var id = scenarioContext.Get<Guid>(CoinKeys.DATABASE_COIN_ID);
            var expectedCoin = scenarioContext.Get<RecivedCoin>(CoinKeys.INPUT_COIN);

            var actualCoin = await coinAction.Get(id);

            actualCoin.Id.Should().Be(id);
            actualCoin.Symbol.Should().Be(expectedCoin.Symbol);
            actualCoin.Name.Should().Be(expectedCoin.Name);
            actualCoin.Value.Should().Be(expectedCoin.Value);
        }


        [Then(@"I recive the requested coin")]
        public async Task ThenIReciveTheRequestedCoinAsync()
        {
            var result = scenarioContext.Get<HttpResponseMessage>(ActionKeys.ACTION_RESULT);
            Coin? coin = await result.Content.ReadFromJsonAsync<Coin>();
            Coin expectedCoin = scenarioContext.Get<Coin>(CoinKeys.DATABASE_COIN);
            coin.Should().Be(expectedCoin);
        }

        [Then(@"Coin is deleted")]
        public async Task ThenCoinIsDeletedAsync()
        {
            var id = scenarioContext.Get<Guid>(CoinKeys.DATABASE_COIN_ID);
            var result = await coinAction.Exist(id);

            result.Should().BeFalse();
        }

        [Then(@"I recive all the coin")]
        public async Task ThenIReciveAllTheCoinAsync()
        {
            List<Coin> sentCoins = scenarioContext.Get<List<Coin>>(CoinKeys.DATABASE_COIN);

            var result = scenarioContext.Get<HttpResponseMessage>(ActionKeys.ACTION_RESULT);
            IEnumerable<Coin> coins = await result.Content.ReadFromJsonAsync<List<Coin>>();

            coins.Should().Equal(sentCoins);
        }

        [Then(@"Recive the coin sent with a new id created")]
        public async Task ThenReciveTheCoinVauleWithANewIdCreatedAsync()
        {
            var result = scenarioContext.Get<HttpResponseMessage>(ActionKeys.ACTION_RESULT);
            var coin = await result.Content.ReadFromJsonAsync<Coin>();

            coin.Should().NotBeNull();
            coin.Id.Should().NotBeEmpty();

            scenarioContext.Add(CoinKeys.DATABASE_COIN_ID, coin.Id);
        }

        [Then(@"The coins are in the coin list")]
        public async Task ThenTheCoinsAreInTheCoinListAsync()
        {
            IEnumerable<Coin> coins = await coinAction.GetAll();
            List<RecivedCoin> sentCoins = scenarioContext.Get<List<RecivedCoin>>(CoinKeys.INPUT_COIN);

            List<RecivedCoin> recivedCoins = coins.Select(x => (RecivedCoin)x).ToList();

            foreach (RecivedCoin c in sentCoins)
            {
                recivedCoins.Should().ContainEquivalentOf(c);
            }
        }

    }
}
