using API.SDK.Domain.Exceptions;
using API.Wallets.Domain.Entities.CoinEntity;
using API.Wallets.Domain.Entities.CoinEntity.ValueObjects;
using API.Wallets.Domain.ErrorCodes;

namespace API.Wallets.UnitTests.System.Domain.CoinEntity;

internal class CoinTest
{
    private CoinSymbol symbol;
    private CoinName name;
    private CoinValue value;

    [SetUp]
    public void Setup()
    {
        symbol = new CoinSymbol(FixureManger.Create<string>());
        name = FixureManger.Create<CoinName>();
        value = FixureManger.Create<CoinValue>();

    }

    [Test]
    public void Create_ShouldCreateACoin()
    {
        var coin = Coin.Create(symbol, name, value);

        Assert.Multiple(() => {
            Assert.That(coin.Id, Is.Not.Empty);
            Assert.That(coin.Symbol, Is.EqualTo(symbol));
            Assert.That(coin.Name, Is.EqualTo(name));
            Assert.That(coin.Value, Is.EqualTo(value));
        });
    }

    [Test]
    public void Create_ShouldTriggerAnError_WhenSymbolIsEmpty()
    {
        var exception = Assert.Throws<DomainStringEmptyException>(() => Coin.Create(new CoinSymbol(""), name, value));
        Assert.Multiple(() =>
        {
            Assert.That(exception.Code, Is.EqualTo(ErrorCode.Coin.SimbolIsEmpty.Code));
            Assert.That(exception.Description, Is.EqualTo(ErrorCode.Coin.SimbolIsEmpty.Description));
        });
    }

    [Test]
    public void Create_ShouldTriggerAnError_WhenNameIsEmpty()
    {
        var exception = Assert.Throws<DomainStringEmptyException>(() => Coin.Create(symbol, new CoinName(""), value));
        Assert.Multiple(() =>
        {
            Assert.That(exception.Code, Is.EqualTo(ErrorCode.Coin.NameIsEmpty.Code));
            Assert.That(exception.Description, Is.EqualTo(ErrorCode.Coin.NameIsEmpty.Description));
        });
    }

    [Test]
    public void Create_ShouldTriggerAnError_WhenValueIsNegative()
    {
        var exception = Assert.Throws<DomainNumberNegativeException>(() => Coin.Create(symbol, name, new CoinValue(-1)));
        Assert.Multiple(() =>
        {
            Assert.That(exception.Code, Is.EqualTo(ErrorCode.Coin.ValueIsNegative.Code));
            Assert.That(exception.Description, Is.EqualTo(ErrorCode.Coin.ValueIsNegative.Description));
        });
    }
}
