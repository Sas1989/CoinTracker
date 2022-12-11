using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions;
using CoinTracker.API.CoinList.Acceptance.Support.Wallets;
using CoinTracker.API.CoinList.Acceptance.Support.Wallets.Models;
using System;
using System.Net.Http.Json;
using TechTalk.SpecFlow;

namespace CoinTracker.API.CoinList.Acceptance.StepDefinitions.Wallets
{
    [Binding]
    public class WalletCrudStepDefinitions : BaseStepDefinition
    {
        private readonly WalletsActions walletAction;

        public WalletCrudStepDefinitions(ScenarioContext scenarioContext, HttpClient httpClient) : base(scenarioContext, httpClient)
        {
            this.walletAction = ActionFactory.GetAction<WalletsActions>();
        }

        [Given(@"A new wallet with name (.*)")]
        public void GivenANewWalletWithNameName(string name)
        {
            var wallet = new RecivedWallet { Name = name };
            scenarioContext.Add(WalletsKeys.INPUT_WALLET, wallet);
        }

        [Given(@"a (.*) as description")]
        public void GivenADescriptionAsDescription(string description)
        {
            var wallet = scenarioContext.Get<RecivedWallet>(WalletsKeys.INPUT_WALLET);
            wallet.Description = description;
            scenarioContext[WalletsKeys.INPUT_WALLET] = wallet;
        }

        [When(@"I post this wallet")]
        public async Task WhenIPushThisWalletAsync()
        {
            var data = scenarioContext.Get<RecivedWallet>(WalletsKeys.INPUT_WALLET);
            var result = await httpClient.PostAsJsonAsync(WalletsEndPoint.API_WALLETS, data);
            scenarioContext.Add(WalletsKeys.ACTION_RESULT, result);

        }

        [Then(@"Post action return Ok")]
        public async Task ThenPostActionReturnOkAsync()
        {
            var result = scenarioContext.Get<HttpResponseMessage>(WalletsKeys.ACTION_RESULT);
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var wallet = await result.Content.ReadFromJsonAsync<Wallet>();

            wallet.Should().NotBeNull();
            wallet.Id.Should().NotBeEmpty();

            scenarioContext.Add(WalletsKeys.CREATE_WALLET_ID, wallet.Id);

        }


        [Then(@"The wallet is well saved")]
        public async Task ThenTheWalletIsWellSavedAsync()
        {
            var walletId = scenarioContext.Get<Guid>(WalletsKeys.CREATE_WALLET_ID);
            var expectedWallet = scenarioContext.Get<RecivedWallet>(WalletsKeys.INPUT_WALLET);

            var savedWallet = (RecivedWallet)await walletAction.Get(walletId);

            savedWallet.Should().Be(expectedWallet);

        }
    }
}
