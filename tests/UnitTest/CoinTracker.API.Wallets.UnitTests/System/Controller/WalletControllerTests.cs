﻿using CoinTracker.API.Wallets.Application.Services.Interfaces;
using CoinTracker.API.Wallets.Controllers;
using CoinTracker.API.Wallets.Domain.Dtos;
using CoinTracker.API.Wallets.UnitTests.Fixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.Wallets.UnitTests.System.Controller
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


            walletService.Setup(service => service.CreateAsync(recivedWallet)).ReturnsAsync(walletDto);
            walletService.Setup(service => service.GetWalletAsync(walletId)).ReturnsAsync(walletDto);
            walletService.Setup(service => service.DeleteWalletAsync(walletId)).ReturnsAsync(true);
            walletService.Setup(service => service.GetWalletAsync()).ReturnsAsync(walletDtoList);
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

            walletService.Verify(service => service.GetWalletAsync(walletId), Times.Once);
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
            walletService.Setup(service => service.GetWalletAsync(walletId)).ReturnsAsync(nullWalletDto);

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

            walletService.Verify(service => service.DeleteWalletAsync(walletId), Times.Once);
        }

        [Test]
        public async Task DelteAsync_WalletNotFound_Return404()
        {
            walletService.Setup(service => service.DeleteWalletAsync(walletId)).ReturnsAsync(false);

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

            walletService.Verify(service => service.GetWalletAsync(), Times.Once);
        }

        [Test]
        public async Task GetAsyncs_Return_WalledDtoIEnumerable()
        {
            var ret = await wallertController.GetAsync();

            var walletActual = (OkObjectResult)ret;

            Assert.AreEqual(walletDtoList, walletActual.Value);
        }
    }
}