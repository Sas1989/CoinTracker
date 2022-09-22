using CoinTracker.API.CoinList.Application.Services.Interfaces;
using CoinTracker.API.CoinList.Controllers.Controllers;
using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.UnitTests.Fixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.UnitTests.System.Controllers
{
    internal class CoinListControllerTests
    {
        private Mock<ICoinService> coinservice;
        private CoinListController controller;

        [SetUp]
        public void Setup()
        {
            coinservice = new Mock<ICoinService>();
            controller = new CoinListController(coinservice.Object);
        }
        [Test]
        public async Task GetAsync_Rerurn_200Async()
        {
            var result = await controller.GetAsync();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
            var objresult = (OkObjectResult)result;
            Assert.AreEqual(objresult.StatusCode, 200);

        }

        [Test]
        public async Task GetAsync_GetAllCoinsAsync_CalledOneTime()
        {
            var result = await controller.GetAsync();

            coinservice.Verify(coinservice => coinservice.GetAllCoinsAsync(), Times.Once());
        }

        [Test]
        public async Task GetAsync_Return_ListOfCoinDto()
        {
            var coinsDto = CoinFixture.GenereteListOfCoinDtos();

            coinservice.Setup(coinservice => coinservice.GetAllCoinsAsync()).ReturnsAsync(coinsDto);

            var result = await controller.GetAsync();

            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
            var objresult = (OkObjectResult)result;
            Assert.That(objresult.Value, Is.InstanceOf(typeof(IEnumerable<CoinDto>)));
            var expectedcoins = (IEnumerable<CoinDto>)objresult.Value;
            Assert.AreEqual(expectedcoins.Count(), coinsDto.Count());
        }

        [Test]
        public async Task GetByIdAsync_GetCoinAsync_CalledOneTime()
        {
            var id = Guid.NewGuid();

            var result = await controller.GetByIdAsync(id);

            coinservice.Verify(coinservice => coinservice.GetCoinAsync(id), Times.Once());
        }

        [Test]
        public async Task GetByIdAsync_CoinNotFound_Return404()
        {
            var id = Guid.NewGuid();
            CoinDto coin = null;

            coinservice.Setup(coinservice => coinservice.GetCoinAsync(id)).ReturnsAsync(coin);

            var result = await controller.GetByIdAsync(id);

            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)));
            var objresult = (NotFoundResult)result;
            Assert.AreEqual(objresult.StatusCode, 404);
        }


        [Test]
        public async Task GetByIdAsync_Retrun_CoinDto()
        {
            CoinDto coinDtos = CoinFixture.GenerateCoinDtos();

            coinservice.Setup(coinservice => coinservice.GetCoinAsync(coinDtos.Id)).ReturnsAsync(coinDtos);

            var result = await controller.GetByIdAsync(coinDtos.Id);

            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
            var objresult = (OkObjectResult)result;
            Assert.That(objresult.Value, Is.TypeOf(typeof(CoinDto)));
            Assert.AreEqual(objresult.Value, coinDtos);
        }

        [Test]
        public async Task PostAsync_CreateAsync_CalledOneTime()
        {
            RecivedCoinDto recivedCoin = CoinFixture.GenerateRecivedDtos();
            CoinDto coin = CoinFixture.GenerateCoinDtos();

            coinservice.Setup(coinservice => coinservice.CreateAsync(recivedCoin)).ReturnsAsync(coin);

            var result = await controller.PostAsync(recivedCoin);

            coinservice.Verify(coinservice => coinservice.CreateAsync(recivedCoin), Times.Once());
        }

        [Test]
        public async Task PostAsync_Returns_200Async()
        {
            RecivedCoinDto recivedCoin = CoinFixture.GenerateRecivedDtos();
            CoinDto coin = CoinFixture.GenerateCoinDtos();

            coinservice.Setup(coinservice => coinservice.CreateAsync(recivedCoin)).ReturnsAsync(coin);

            var result = await controller.PostAsync(recivedCoin);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
            var objresult = (OkObjectResult)result;
            Assert.AreEqual(objresult.StatusCode, 200);
        }

        [Test]
        public async Task PostAsync_Returns_CoinDto()
        {
            RecivedCoinDto recivedCoin = CoinFixture.GenerateRecivedDtos();
            CoinDto coin = CoinFixture.GenerateCoinDtos();

            coinservice.Setup(coinservice => coinservice.CreateAsync(recivedCoin)).ReturnsAsync(coin);

            var result = await controller.PostAsync(recivedCoin);

            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
            var objresult = (OkObjectResult)result;
            Assert.That(objresult.Value, Is.TypeOf(typeof(CoinDto)));
            Assert.AreEqual(objresult.Value, coin);
        }

        [Test]
        public async Task PostMultipleCoinAsync_Returns_200Async()
        {
            IEnumerable<RecivedCoinDto> recivedCoin = CoinFixture.GenereteListOfRecivedDtos();

            var result = await controller.MassiveAsync(recivedCoin);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
            var objresult = (OkObjectResult)result;
            Assert.AreEqual(objresult.StatusCode, 200);
        }

        [Test]
        public async Task PostMultipleCoinAsync_Returns_CoinDtoTypes()
        {
            IEnumerable<RecivedCoinDto> recivedCoin = CoinFixture.GenereteListOfRecivedDtos();
            IEnumerable<CoinDto> coin = CoinFixture.GenereteListOfCoinDtos();

            var result = await controller.MassiveAsync(recivedCoin);

            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
            var objresult = (OkObjectResult)result;
            Assert.That(objresult.Value, Is.InstanceOf(typeof(IEnumerable<CoinDto>)));
        }

        [Test]
        public async Task PostMultipleCoinAsync_CreateMutiple_CalledOneTime()
        {
            IEnumerable<RecivedCoinDto> recivedCoin = CoinFixture.GenereteListOfRecivedDtos();

            var result = await controller.MassiveAsync(recivedCoin);

            coinservice.Verify(x => x.CreateMultipleAsync(recivedCoin), Times.Once);
        }

        [Test]
        public async Task PostMultipleCoinAsync_ReturnS_CoinDtoList()
        {
            IEnumerable<RecivedCoinDto> recivedCoin = CoinFixture.GenereteListOfRecivedDtos();
            IEnumerable<CoinDto> coin = CoinFixture.GenereteListOfCoinDtos();

            coinservice.Setup(x => x.CreateMultipleAsync(recivedCoin)).ReturnsAsync(coin);

            var result = await controller.MassiveAsync(recivedCoin);

            var objresult = (OkObjectResult)result;
            Assert.AreEqual(objresult.Value, coin);
        }
    }
}
