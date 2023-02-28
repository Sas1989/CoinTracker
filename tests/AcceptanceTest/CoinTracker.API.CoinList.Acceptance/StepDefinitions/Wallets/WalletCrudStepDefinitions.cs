using CoinTracker.API.CoinList.Acceptance.Support.Services.ApiActions;
using CoinTracker.API.CoinList.Acceptance.Support.Wallets;
using CoinTracker.API.CoinList.Acceptance.Support.Wallets.Models;
using System;
using System.Net;
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

        [Given(@"A new wallet with name (.*) and description (.*)")]
        public void GivenANewWalletWithNameName(string name, string description)
        {
            var wallet = new RecivedWallet { Name = name , Description = description };
            scenarioContext.Add(WalletsKeys.INPUT_WALLET, wallet);
        }

        [Given(@"An existing wallet with (.*) as name and (.*) as description")]
        public async Task GivenAnExistingWalletWithAsNameAndAsDescriptionAsync(string name, string description)
        {
            var existingWallet = new RecivedWallet { Name = name, Description = description };
            var dbwallet = await walletAction.Create(existingWallet);
            scenarioContext.Add(WalletsKeys.DATABASE_WALLET, dbwallet);
            scenarioContext.Add(WalletsKeys.DATABASE_WALLET_ID, dbwallet.Id);
            scenarioContext.Add(WalletsKeys.INPUT_WALLET, existingWallet);
        }

        [Given(@"a wallet not in the database")]
        public void GivenAWalletNotInTheDatabase()
        {
            var wallet = new RecivedWallet { Name = "Random", Description = "Random" };
            scenarioContext.Add(WalletsKeys.DATABASE_WALLET_ID, Guid.NewGuid());
            scenarioContext.Add(WalletsKeys.INPUT_WALLET, wallet);
        }

        [When(@"I post this wallet")]
        public async Task WhenIPostThisWalletAsync()
        {
            RecivedWallet data = scenarioContext.Get<RecivedWallet>(WalletsKeys.INPUT_WALLET);
            var result = await httpClient.PostAsJsonAsync(WalletsEndPoint.API_WALLETS, data);
            scenarioContext.Add(WalletsKeys.ACTION_RESULT, result);

        }

        [When(@"I put this wallet")]
        public async Task WhenIPushThisWalletAsync()
        {
            RecivedWallet data = scenarioContext.Get<RecivedWallet>(WalletsKeys.INPUT_WALLET);
            Guid id = scenarioContext.Get<Guid>(WalletsKeys.DATABASE_WALLET_ID);
            var result = await httpClient.PutAsJsonAsync($"{WalletsEndPoint.API_WALLETS}/{id}", data);
            scenarioContext.Add(WalletsKeys.ACTION_RESULT, result);

        }

        [When(@"I update name with (.*)")]
        public void WhenIUpdateNameWithNewName(string newValue)
        {
            RecivedWallet existWallet = scenarioContext.Get<RecivedWallet>(WalletsKeys.INPUT_WALLET);
            existWallet.Name = newValue;

            scenarioContext[WalletsKeys.INPUT_WALLET] = existWallet;
        }

        [When(@"I update description with (.*)")]
        public void WhenIUpdateDescriptionWithNewName(string newValue)
        {
            RecivedWallet existWallet = scenarioContext.Get<RecivedWallet>(WalletsKeys.INPUT_WALLET);
            existWallet.Description = newValue;

            scenarioContext[WalletsKeys.INPUT_WALLET] = existWallet;
        }

        [Then(@"The wallet is well saved")]
        public async Task ThenTheWalletIsWellSavedAsync()
        {
            var result = scenarioContext.Get<HttpResponseMessage>(WalletsKeys.ACTION_RESULT);
            var wallet = await result.Content.ReadFromJsonAsync<Wallet>();

            var expectedWallet = scenarioContext.Get<RecivedWallet>(WalletsKeys.INPUT_WALLET);

            wallet.Should().NotBeNull();
            wallet.Id.Should().NotBeEmpty();

            var savedWallet = (RecivedWallet)await walletAction.Get(wallet.Id);

            savedWallet.Should().Be(expectedWallet);

        }

    }
}
