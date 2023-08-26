using API.SDK.Application.Repository;
using API.SDK.Domain.Persistence.DataProvider;
using API.Wallets.Domain.Entities.CoinEntity;
using API.Wallets.Domain.Entities.WalletEntity;
using API.Wallets.Domain.ErrorCodes;
using API.Wallets.Domain.Exception;
using API.Wallets.Infrastructure.Repositories;
using AutoFixture;
using System.Linq.Expressions;

namespace API.Wallets.UnitTests.System.Infrastructure.Repositories;

internal class WalletRepositoryTest
{
    private IDataProvider<Wallet> dataProvider;
    private IRepository<Coin> coinRepository;
    private Wallet wallet;
    private decimal numberOfCoin;
    private Coin coin;
    private Guid coinId;
    private WalletRepository walletRepository;

    [SetUp]
    public void SetupAsync()
    {
        dataProvider = Substitute.For<IDataProvider<Wallet>>();
        coinRepository = Substitute.For<IRepository<Coin>>();
        wallet = FixureManger.Create<Wallet>();
        numberOfCoin = FixureManger.Create<decimal>();

        coin = FixureManger.Create<Coin>();
        coinId = coin.Id;

        walletRepository = new WalletRepository(dataProvider, coinRepository);

        coinRepository.GetAsync(coinId).Returns(coin);
    }

    [Test]
    public async Task AddCoinAsync_GetCoin_CalledOnce()
    {

        await walletRepository.AddCoinAsync(wallet,coinId,numberOfCoin);

        await coinRepository.Received(1).GetAsync(coinId);
    }

    [Test]
    public void AddCoinAsync_GetCoinReturnNull_ReturnNull()
    {
        coinRepository.GetAsync(coinId).Returns((Coin)null!);

        var exception = Assert.ThrowsAsync<CoinNotFoundException>(async () => await walletRepository.AddCoinAsync(wallet, coinId, numberOfCoin));
        
        Assert.Multiple(() =>
        {
            Assert.That(exception.Code, Is.EqualTo(ErrorCode.Coin.NotFound.Code));
            Assert.That(exception.Description, Is.EqualTo(ErrorCode.Coin.NotFound.Description));
        });

    }

    [Test]
    public async Task AddCoinAsync_Should_AddCoin()
    {
        var nCoin = wallet.Coins.Count;

        await walletRepository.AddCoinAsync(wallet, coinId, numberOfCoin);

        var resultCoin = wallet.Coins.FirstOrDefault(c => c.Id == coinId);
        Assert.Multiple(() =>
        {
            Assert.That(wallet.Coins, Has.Count.EqualTo(nCoin + 1));
            Assert.That(resultCoin, Is.Not.Null);
            Assert.That(resultCoin?.Quantity.Value, Is.EqualTo(numberOfCoin));
        });
    }
    [Test]
    public async Task AddCoinAsync_Update_CalledOnce()
    {
        await walletRepository.AddCoinAsync(wallet, coinId, numberOfCoin);

        await dataProvider.Received(1).SaveAsync(wallet);
    }

    [Test]
    public async Task AddCoinAsync_GetCoinReturnNull_UpdateNeverCalled()
    {
        coinRepository.GetAsync(coinId).Returns((Coin)null!);

        Assert.ThrowsAsync<CoinNotFoundException>(async() => await walletRepository.AddCoinAsync(wallet, coinId, numberOfCoin));

        await dataProvider.DidNotReceive().SaveAsync(wallet);

    }

    [Test]
    public async Task WalletNameIsUnique_ShouldReturnTrue_WhenCountIsZero()
    {
        dataProvider.Count(Arg.Any<Expression<Func<Wallet, bool>>>()).Returns(0);

        var result = await walletRepository.WalletNameIsUnique(wallet.Name);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task WalletNameIsUnique_ShouldReturnTrue_WhenCountMoreThenZero()
    {
        dataProvider.Count(Arg.Any<Expression<Func<Wallet, bool>>>()).Returns(FixureManger.Create<int>());

        var result = await walletRepository.WalletNameIsUnique(wallet.Name);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetWalletBalance_ShouldGetCoin_ForEachCoin()
    {
        dataProvider.Count(Arg.Any<Expression<Func<Wallet, bool>>>()).Returns(FixureManger.Create<int>());

        var result = await walletRepository.WalletNameIsUnique(wallet.Name);

        Assert.That(result, Is.False);
    }
}
