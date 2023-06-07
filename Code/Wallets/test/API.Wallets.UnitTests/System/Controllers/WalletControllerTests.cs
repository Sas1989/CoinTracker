using API.SDK.Domain.Exceptions;
using API.Wallets.Application.Services.Interfaces;
using API.Wallets.Controllers;
using API.Wallets.Domain.Dtos.Wallet;
using API.Wallets.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.UnitTests.System.Controllers
{
    public class WalletControllerTests
    {
        private Mock<IWalletService> walletService;
        private WalletController wallertController;
        private RecivedWalletDto recivedWallet;
        private WalletDto walletDto;
        private Guid walletId;
        private WalletDto? nullWalletDto;
        private IEnumerable<WalletDto> walletDtoList;
        private IEnumerable<RecivedWalletDto> recivedWalletList;

        [SetUp]
        public void Setup()
        {
            walletService = new Mock<IWalletService>();

            wallertController = new WalletController(walletService.Object);

            recivedWallet = WalletFixture.RecivedWalletDto();
            walletDto = WalletFixture.WalletDto();
            walletId = walletDto.Id;
            nullWalletDto = null;

            walletDtoList = WalletFixture.WalletDtoList();
            recivedWalletList = WalletFixture.RecivedWalletList();


            walletService.Setup(service => service.CreateAsync(recivedWallet)).ReturnsAsync(walletDto);
            walletService.Setup(service => service.UpdateAsync(walletId, recivedWallet)).ReturnsAsync(walletDto);
            walletService.Setup(service => service.GetAsync(walletId)).ReturnsAsync(walletDto);
            walletService.Setup(service => service.DeleteAsync(walletId)).ReturnsAsync(true);
            walletService.Setup(service => service.GetAllAsync()).ReturnsAsync(walletDtoList);
            walletService.Setup(service => service.CreateMultipleAsync(recivedWalletList)).ReturnsAsync(walletDtoList);
        }

        [Test]
        public async Task PostAsync_Retrun_OK()
        {
            var ret = await wallertController.PostAsync(recivedWallet);

            Assert.That(ret, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task PostAsync_Return_WalletDto()
        {
            var ret = await wallertController.PostAsync(recivedWallet);

            var walletActual = (OkObjectResult)ret;

            Assert.AreEqual(walletDto, walletActual.Value);

        }

        [Test]
        public async Task PostAsyc_Create_CalledOnce()
        {

            var ret = await wallertController.PostAsync(recivedWallet);

            walletService.Verify(service => service.CreateAsync(recivedWallet), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_Return_Ok()
        {

            var ret = await wallertController.GetByIdAsync(walletId);

            Assert.That(ret, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task GetByIdAsync_GetAsync_CalledOnce()
        {

            var ret = await wallertController.GetByIdAsync(walletId);

            walletService.Verify(service => service.GetAsync(walletId), Times.Once);
        }

        [Test]
        public async Task GetByIdAsync_Return_WalletDto()
        {

            var ret = await wallertController.GetByIdAsync(walletId);

            var walletActual = (OkObjectResult)ret;

            Assert.AreEqual(walletDto, walletActual.Value);
        }


        [Test]
        public async Task GetByIdAsync_WalletNotFound_Return404()
        {
            walletService.Setup(service => service.GetAsync(walletId)).Throws<EntityNotFoundException>();

            var ret = await wallertController.GetByIdAsync(walletId);

            Assert.That(ret, Is.TypeOf(typeof(NotFoundResult)));
        }

        [Test]
        public async Task DelteAsync_Retrun_Ok()
        {
            var ret = await wallertController.DeleteAsync(walletId);

            Assert.That(ret, Is.TypeOf(typeof(OkResult)));
        }

        [Test]
        public async Task DelteAsync_DeleteWallet_CalledOnce()
        {
            var ret = await wallertController.DeleteAsync(walletId);

            walletService.Verify(service => service.DeleteAsync(walletId), Times.Once);
        }

        [Test]
        public async Task DelteAsync_WalletNotFound_Return404()
        {
            walletService.Setup(service => service.DeleteAsync(walletId)).ReturnsAsync(false);

            var ret = await wallertController.DeleteAsync(walletId);

            Assert.That(ret, Is.TypeOf(typeof(NotFoundResult)));
        }

        [Test]
        public async Task GetAsyncs_Return_OK()
        {
            var ret = await wallertController.GetAsync();

            Assert.That(ret, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task GetAsyncs_GetWallet_CalledOnce()
        {
            var ret = await wallertController.GetAsync();

            walletService.Verify(service => service.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetAsyncs_Return_WalledDtoIEnumerable()
        {
            var ret = await wallertController.GetAsync();

            var walletActual = (OkObjectResult)ret;

            Assert.AreEqual(walletDtoList, walletActual.Value);
        }

        [Test]
        public async Task PutAsync_Retrun_OK()
        {
            var ret = await wallertController.PutAsync(walletId, recivedWallet);

            Assert.That(ret, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task PutAsyc_Create_CalledOnce()
        {

            var ret = await wallertController.PutAsync(walletId, recivedWallet);

            walletService.Verify(service => service.UpdateAsync(walletId, recivedWallet), Times.Once);
        }

        [Test]
        public async Task PutAsync_Return_WalletDto()
        {
            var ret = await wallertController.PutAsync(walletId, recivedWallet);

            var walletActual = (OkObjectResult)ret;

            Assert.AreEqual(walletDto, walletActual.Value);

        }

        [Test]
        public async Task PutAsync_WalletNotFound_Return404()
        {
            walletService.Setup(service => service.UpdateAsync(walletId, recivedWallet)).Throws<EntityNotFoundException>();

            var ret = await wallertController.PutAsync(walletId, recivedWallet);

            Assert.That(ret, Is.TypeOf(typeof(NotFoundResult)));
        }

        [Test]
        public async Task PostBulkAsync_Retrun_OK()
        {
            var ret = await wallertController.BulkAsync(recivedWalletList);

            Assert.That(ret, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task PostBulkAsync_Return_WalletDtoList()
        {
            var ret = await wallertController.BulkAsync(recivedWalletList);

            var walletsActual = (OkObjectResult)ret;

            Assert.AreEqual(walletDtoList, walletsActual.Value);

        }

        [Test]
        public async Task PostBulkAsync_CreateMassive_CalledOnce()
        {

            var ret = await wallertController.BulkAsync(recivedWalletList);

            walletService.Verify(service => service.CreateMultipleAsync(recivedWalletList), Times.Once);
        }
    }
}
