using API.SDK.Domain.Exceptions;
using API.Wallets.Domain.Entities.CoinEntity;
using API.Wallets.Domain.Entities.WalletEntity;
using API.Wallets.Domain.Entities.WalletEntity.ValueObjects;
using API.Wallets.Domain.ErrorCodes;
using API.Wallets.Domain.Exception;
using API.Wallets.Domain.Repositories;

namespace API.Wallets.UnitTests.System.Domain.WalletEntity;

internal class WalletTest
{
    private WalletName name;
    private string desc;
    private Coin coin;
    private WalletQuantity numberOfCoin;
    private Wallet existingWallet;

    [SetUp]
    public void Setup()
    {
        name = FixureManger.Create<WalletName>();
        desc = FixureManger.Create<string>();
        coin = FixureManger.Create<Coin>();
        numberOfCoin = FixureManger.Create<WalletQuantity>();

        existingWallet = Wallet.Create(name, desc);

    }
    [Test]
    public void Create_Should_CreateTheObjcetCorrectlyAsync()
    {
        var wallet = Wallet.Create(name, desc);

        Assert.That(wallet, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(wallet.Name, Is.EqualTo(name));
            Assert.That(wallet.Description, Is.EqualTo(desc));
            Assert.That(wallet.Coins, Is.Empty);
        });
    }

    [Test]
    public void Create_Should_IdNotEmptyAsync()
    {

        var wallet = Wallet.Create(name, desc);

        Assert.That(wallet, Is.Not.Null);
        Assert.That(wallet.Id, Is.Not.Empty);
    }

    [Test]
    public void Create_ShouldTriggerAnExctgpetion_WhenNameIsEmpty()
    {

        var exception = Assert.Throws<DomainStringEmptyException>(() => Wallet.Create(new WalletName(""), desc));
        Assert.Multiple(() =>
        {
            Assert.That(exception.Code, Is.EqualTo(ErrorCode.Wallet.NameIsEmpty.Code));
            Assert.That(exception.Description, Is.EqualTo(ErrorCode.Wallet.NameIsEmpty.Description));
        });
    }

    [Test]
    public void AddNewCoin_Should_AddTheCoinToAWallet()
    {
        existingWallet?.AddNewCoin(coin, numberOfCoin);

        Assert.That(existingWallet?.Coins.Count, Is.EqualTo(1));

        var wCoin = existingWallet.Coins[0];
        Assert.Multiple(() =>
        {
            Assert.That(wCoin.Id, Is.EqualTo(coin.Id));
            Assert.That(wCoin.Name, Is.EqualTo(coin.Name));
            Assert.That(wCoin.Symbol, Is.EqualTo(coin.Symbol));
            Assert.That(wCoin.Quantity, Is.EqualTo(numberOfCoin));
        });

    }

    [Test]
    public void AddNewCoin_ShouldTriggerAnError_WhenTheSameCoinIsAdded()
    {
        existingWallet?.AddNewCoin(coin, numberOfCoin);

        var exception = Assert.Throws<CoinAlreadyInTheWallet>(() => existingWallet?.AddNewCoin(coin, numberOfCoin));
        Assert.Multiple(() =>
        {
            Assert.That(exception.Code, Is.EqualTo(ErrorCode.Wallet.CoinAlreadyInTheWallet.Code));
            Assert.That(exception.Description, Is.EqualTo(ErrorCode.Wallet.CoinAlreadyInTheWallet.Description));
        });
    }
}