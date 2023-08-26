using API.SDK.Domain.Exceptions;
using API.Wallets.Domain.Entities.CoinEntity;
using API.Wallets.Domain.Entities.WalletEntity;
using API.Wallets.Domain.Entities.WalletEntity.ValueObjects;
using API.Wallets.Domain.ErrorCodes;

namespace API.Wallets.UnitTests.System.Domain.WalletEntity;

internal class WalletCoinTest
{
    [Test]
    public void Create_Should_CreateCoin()
    {
        var coin = FixureManger.Create<Coin>();
        WalletQuantity number = FixureManger.Create<WalletQuantity>();

        var walletCoin = WalletCoin.Create(coin, number);

        Assert.That(walletCoin, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(walletCoin.Id, Is.EqualTo(coin.Id));
            Assert.That(walletCoin.Symbol, Is.EqualTo(coin.Symbol));
            Assert.That(walletCoin.Name, Is.EqualTo(coin.Name));
            Assert.That(walletCoin.Quantity, Is.EqualTo(number));
        });
    }

    [Test]
    public void Create_ShouldTriggerAnError_WhenQuantityIsNegative()
    {
        var coin = FixureManger.Create<Coin>();

        var exception = Assert.Throws<DomainNumberNegativeException>(() => WalletCoin.Create(coin, new WalletQuantity(-1)));
        Assert.Multiple(() =>
        {
            Assert.That(exception.Code, Is.EqualTo(ErrorCode.Wallet.QuantityIsNegative.Code));
            Assert.That(exception.Description, Is.EqualTo(ErrorCode.Wallet.QuantityIsNegative.Description));
        });
    }
}
