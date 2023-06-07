using API.CoinList.Acceptance.Support.Wallets.Models;
using CoinTracker.API.CoinList.Acceptance.StepDefinitions;
using CoinTracker.API.CoinList.Acceptance.Support.Action;
using CoinTracker.API.CoinList.Acceptance.Support.CoinList;
using CoinTracker.API.CoinList.Acceptance.Support.CoinList.Models;
using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions;
using CoinTracker.API.CoinList.Acceptance.Support.Wallets;
using System;
using System.Net.Http.Json;
using TechTalk.SpecFlow;

namespace API.CoinList.Acceptance.StepDefinitions
{
    [Binding]
    public class WalletCoinManagerStepDefinitions : BaseStepDefinition
    {
        private WalletsActions walletAction;
        private CoinAction coinAction;

        public WalletCoinManagerStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient) : base(scenarioContext, httpClient)
        {
            this.walletAction = ActionFactory.GetAction<WalletsActions>();
            this.coinAction = ActionFactory.GetAction<CoinAction>();
        }

        [When(@"I add (.*) (.*) to the wallet")]
        public async Task WhenIAddBTCToTheWalletAsync(Decimal numberValue, string coinSymbol)
        {
            var coinList = scenarioContext.Get<IEnumerable<Coin>>(CoinKeys.DATABASE_COIN);
            var coin = coinList.FirstOrDefault(coin => coin.Symbol == coinSymbol);

            var walletID = scenarioContext.Get<Guid>(WalletsKeys.DATABASE_WALLET_ID);

            var walletCoin = new WalletCoinsInput(coin.Id, numberValue);

            var endPoint = string.Format(WalletsEndPoint.API_WALLETS_COINS, walletID);

            var result = await httpClient.PostAsJsonAsync(endPoint, walletCoin);

            scenarioContext.Add(WalletsKeys.INPUT_COIN_WALLET, walletCoin);
            scenarioContext.Add(ActionKeys.ACTION_RESULT, walletCoin);

        }

        [Then(@"Wallet sould have inside the correct value of coin")]
        public async Task ThenWalletSouldHaveInsideTheCorrectValueOfCoinAsync()
        {
            var walletID = scenarioContext.Get<Guid>(WalletsKeys.DATABASE_WALLET_ID);
            var wallet = await walletAction.Get(walletID);
            var walletCoinsInput = scenarioContext.Get<WalletCoinsInput>(WalletsKeys.INPUT_COIN_WALLET);
            var coin = await coinAction.Get(walletCoinsInput.coinId);

            var coinInWallet = wallet?.WalletCoins?.FirstOrDefault(c => c.Symbol == coin.Symbol);

            coinInWallet.Should().NotBeNull();
            coinInWallet.NumberOfCoin.Should().Be(walletCoinsInput.numberOfCoin);

        }

        [Then(@"His total value should be (.*)")]
        public async Task ThenHisTotalValueShouldBeAsync(Decimal totalValue)
        {
            var walletID = scenarioContext.Get<Guid>(WalletsKeys.DATABASE_WALLET_ID);
            var wallet = await walletAction.Get(walletID);

            wallet.TotalValue.Should().Be(totalValue);
        }
    }
}
