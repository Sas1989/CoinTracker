using API.Wallets.Application.Services;
using API.Wallets.Controllers;
using API.Wallets.Domain.Dtos.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace API.Wallets.UnitTests.System.Controllers
{
    public class WalletCoinControllerTests
    {
        private WalletCoinController controller;
        private WalletCoinDtoInput recivedWalletCoinDto;
        private Guid walletId;
        private WalletDto wallet;
        private Guid notFoundWallet;
        private Mock<IWalletService> walletServices;

        [SetUp]
        public void SetUp()
        {
            walletServices = new Mock<IWalletService>();
            controller = new WalletCoinController(walletServices.Object);
            recivedWalletCoinDto = FixureManger.Create<WalletCoinDtoInput>();
            wallet = FixureManger.Create<WalletDto>();
            walletId = wallet.Id;
            notFoundWallet = Guid.NewGuid();

            walletServices.Setup(service => service.AddCoin(walletId, recivedWalletCoinDto)).ReturnsAsync(wallet);
            walletServices.Setup(service => service.AddCoin(notFoundWallet, recivedWalletCoinDto)).ReturnsAsync(default(WalletDto));
        }
        [Test]
        public async Task PostAsync_Return_OkAsync()
        {
            var ret = await controller.PostAsync(walletId, recivedWalletCoinDto);

            Assert.That(ret, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task PostAsync_CallAddCoin_Once()
        {
            var ret = await controller.PostAsync(walletId, recivedWalletCoinDto);
            
            walletServices.Verify(service => service.AddCoin(walletId, recivedWalletCoinDto));
        }

        [Test]
        public async Task PostAsync_Return_WalletDto()
        {
            var ret = await controller.PostAsync(walletId, recivedWalletCoinDto);

            var walletActual = (OkObjectResult)ret;

            Assert.That(walletActual.Value, Is.EqualTo(wallet));
        }
        
        [Test]
        public async Task PostAsync_WalletNotFound_Return404()
        {

            var ret = await controller.PostAsync(notFoundWallet, recivedWalletCoinDto);

            Assert.That(ret, Is.TypeOf(typeof(NotFoundResult)));
        }
    }
}
