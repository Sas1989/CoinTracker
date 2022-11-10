using CoinTracker.API.CoinList.Application.Services.Interfaces;
using CoinTracker.API.CoinList.Controllers.Controllers;
using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.UnitTests.Fixture;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CoinTracker.API.CoinList.UnitTests.System.Controllers
{
    internal class CoinListControllerTests
    {
        private Mock<ICoinService> coinservice;
        private CoinListController controller;
        private IEnumerable<CoinDto> coinDtoList;
        private CoinDto coinDto;
        private RecivedCoinDto recivedCoin;
        private IEnumerable<RecivedCoinDto> recivedCoinList;
        private Guid newId;
        private Guid updateId;
        private CoinDto nullCoinDto;
        private string symbol;

        [SetUp]
        public void Setup()
        {

            coinservice = new Mock<ICoinService>();
            controller = new CoinListController(coinservice.Object);

            coinDtoList = CoinFixture.GenereteListOfCoinDtos();
            coinDto = CoinFixture.GenerateCoinDtos();
            recivedCoin = CoinFixture.GenerateRecivedDtos();
            recivedCoinList = CoinFixture.GenereteListOfRecivedDtos();

            newId = Guid.NewGuid();
            updateId = coinDto.Id;
            nullCoinDto = null;
            symbol = coinDto.Symbol;

            coinservice.Setup(coinservice => coinservice.GetAllCoinsAsync()).ReturnsAsync(coinDtoList);
            coinservice.Setup(coinservice => coinservice.GetCoinAsync(newId)).ReturnsAsync(nullCoinDto);
            coinservice.Setup(coinservice => coinservice.GetCoinAsync(coinDto.Id)).ReturnsAsync(coinDto);
            coinservice.Setup(coinservice => coinservice.CreateAsync(recivedCoin)).ReturnsAsync(coinDto);
            coinservice.Setup(coinservice => coinservice.CreateMultipleAsync(recivedCoinList)).ReturnsAsync(coinDtoList);
            coinservice.Setup(coinservice => coinservice.GetCoinAsync(symbol)).ReturnsAsync(coinDto);
            coinservice.Setup(coinservice => coinservice.UpdateCoin(updateId, recivedCoin)).ReturnsAsync(coinDto);
            coinservice.Setup(coinservice => coinservice.UpdateCoin(newId, recivedCoin)).ReturnsAsync(nullCoinDto);
            coinservice.Setup(coinservice => coinservice.UpdateCoin(symbol, recivedCoin)).ReturnsAsync(coinDto);
            coinservice.Setup(coinservice => coinservice.DeleteCoin(newId)).ReturnsAsync(false);
            coinservice.Setup(coinservice => coinservice.DeleteCoin(coinDto.Id)).ReturnsAsync(true);
        }

        [Test]
        public async Task GetAsync_Rerurn_200Async()
        {
            var result = await controller.GetAsync();

            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));

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
            var result = await controller.GetAsync();

            var objresult = (OkObjectResult)result;

            Assert.That(objresult.Value, Is.InstanceOf(typeof(IEnumerable<CoinDto>)));
            Assert.AreEqual(coinDtoList, objresult.Value);
        }

        [Test]
        public async Task GetByIdAsync_GetCoinAsync_CalledOneTime()
        {
            var result = await controller.GetByIdAsync(newId);

            coinservice.Verify(coinservice => coinservice.GetCoinAsync(newId), Times.Once());
        }

        [Test]
        public async Task GetByIdAsync_CoinNotFound_Return404()
        {
            var result = await controller.GetByIdAsync(newId);

            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)));
        }


        [Test]
        public async Task GetByIdAsync_Retrun_CoinDto()
        {

            var result = await controller.GetByIdAsync(coinDto.Id);

            var objresult = (OkObjectResult)result;

            Assert.AreEqual(coinDto, objresult.Value);
        }

        [Test]
        public async Task PostAsync_CreateAsync_CalledOneTime()
        {

            var result = await controller.PostAsync(recivedCoin);

            coinservice.Verify(coinservice => coinservice.CreateAsync(recivedCoin), Times.Once());
        }

        [Test]
        public async Task PostAsync_Returns_200Async()
        {

            var result = await controller.PostAsync(recivedCoin);

            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task PostAsync_Returns_CoinDto()
        {
            var result = await controller.PostAsync(recivedCoin);

            var objresult = (OkObjectResult)result;
            Assert.AreEqual(coinDto, objresult.Value);
        }

        [Test]
        public async Task PostMultipleCoinAsync_Returns_200Async()
        {

            var result = await controller.BulkAsync(recivedCoinList);

            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task PostMultipleCoinAsync_Returns_CoinDtoTypes()
        {
            var result = await controller.BulkAsync(recivedCoinList);

            var objresult = (OkObjectResult)result;
            Assert.That(objresult.Value, Is.InstanceOf(typeof(IEnumerable<CoinDto>)));
        }

        [Test]
        public async Task PostMultipleCoinAsync_CreateMutiple_CalledOneTime()
        {
            var result = await controller.BulkAsync(recivedCoinList);

            coinservice.Verify(x => x.CreateMultipleAsync(recivedCoinList), Times.Once);
        }

        [Test]
        public async Task PostMultipleCoinAsync_Returns_CoinDtoList()
        {
            var result = await controller.BulkAsync(recivedCoinList);

            var objresult = (OkObjectResult)result;
            Assert.AreEqual(coinDtoList, objresult.Value);
        }

        [Test]
        public async Task GetBySimbol_Returns_200()
        {
            var result = await controller.GetBySimbolAsync(symbol);

            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task GetBySimbol_GetCoinBySymbol_CalledOnce()
        {
            var result = await controller.GetBySimbolAsync(symbol);

            coinservice.Verify(x => x.GetCoinAsync(symbol), Times.Once);
        }
        [Test]
        public async Task GetBySimbol_Retruns_CoinDto()
        {
            var result = await controller.GetBySimbolAsync(symbol);

            var objresult = (OkObjectResult)result;
            Assert.AreEqual(coinDto, objresult.Value);
        }

        [Test]
        public async Task GetBySimbol_Retruns_404_WhenNotFound()
        {
            coinservice.Setup(x => x.GetCoinAsync(symbol)).ReturnsAsync(nullCoinDto);

            var result = await controller.GetBySimbolAsync(symbol);

            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)));

        }

        [Test]
        public async Task PutAsync_Returns_200Code()
        {
            var result = await controller.PutAsync(updateId, recivedCoin);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task PutAsync_UpdateCoin_CalledOnce()
        {
            var result = await controller.PutAsync(updateId, recivedCoin);

            coinservice.Verify(x => x.UpdateCoin(updateId, recivedCoin), Times.Once);
        }

        [Test]
        public async Task PutAsync_Returns_CoinDto()
        {
            var result = await controller.PutAsync(updateId, recivedCoin);

            var objresult = (OkObjectResult)result;

            Assert.AreEqual(coinDto, objresult.Value);
        }
        [Test]
        public async Task PutAsync_CoinNotFund_Returns_404Code()
        {
            var result = await controller.PutAsync(newId, recivedCoin);

            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)));
        }

        [Test]
        public async Task PutBySymbolAsync_Returns_200Code()
        {
            var result = await controller.PutBySymbolAsync(symbol, recivedCoin);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task PutBySymbolAsync_UpdateCoin_CalledOnce()
        {
            var result = await controller.PutBySymbolAsync(symbol, recivedCoin);

            coinservice.Verify(x => x.UpdateCoin(symbol, recivedCoin), Times.Once);
        }

        [Test]
        public async Task PutBySymbolAsync_Returns_CoinDto()
        {
            var result = await controller.PutBySymbolAsync(symbol, recivedCoin);

            var objresult = (OkObjectResult)result;

            Assert.AreEqual(coinDto, objresult.Value);
        }
        [Test]
        public async Task PutBySymbolAsync_CoinNotFund_Returns_404Codee()
        {
            coinservice.Setup(coinservice => coinservice.UpdateCoin(symbol, recivedCoin)).ReturnsAsync(nullCoinDto);

            var result = await controller.PutBySymbolAsync(symbol, recivedCoin);

            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)));

        }

        [Test]
        public async Task Delete_Returns_Returns_200Code()
        {
            var result = await controller.DeleteAsync(coinDto.Id);

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task Delete_Returns_DeleteCoin_CalledOnce()
        {
            var result = await controller.DeleteAsync(coinDto.Id);

            coinservice.Verify(x => x.DeleteCoin(coinDto.Id), Times.Once);
        }

        [Test]
        public async Task Delete_Returns_DeleteCoin_ReturnFalse_Retruns_404Code()
        {
            var result = await controller.DeleteAsync(newId);

            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)));
        }
    }
}
