using API.CoinList.Application.Common.Publishers;
using API.CoinList.Application.Services;
using API.CoinList.Controllers;
using API.CoinList.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.CoinList.UnitTests.System.Controllers
{
    internal class CoinListControllerTests
    {
        private Mock<ICoinService> coinservice;
        private Mock<ICoinPublisher> coinPublisher;
        private CoinListController controller;
        private IEnumerable<CoinDto> coinDtoList;
        private CoinDto coinDto;
        private CoinDtoInput recivedCoin;
        private IEnumerable<CoinDtoInput> recivedCoinList;
        private Guid newId;
        private Guid updateId;
        private CoinDto nullCoinDto;
        private string symbol;

        [SetUp]
        public void Setup()
        {

            coinservice = new Mock<ICoinService>();
            coinPublisher = new Mock<ICoinPublisher>();
            controller = new CoinListController(coinservice.Object, coinPublisher.Object);

            coinDtoList = FixureManger.CreateList<CoinDto>();
            coinDto = FixureManger.Create<CoinDto>();
            recivedCoin = FixureManger.Create<CoinDtoInput>();
            recivedCoinList = FixureManger.CreateList<CoinDtoInput>();

            newId = Guid.NewGuid();
            updateId = coinDto.Id;
            nullCoinDto = default;
            symbol = coinDto.Symbol;

            coinservice.Setup(coinservice => coinservice.GetAllAsync()).ReturnsAsync(coinDtoList);

            coinservice.Setup(coinservice => coinservice.GetAsync(coinDto.Id)).ReturnsAsync(coinDto);
            coinservice.Setup(coinservice => coinservice.GetAsync(symbol)).ReturnsAsync(coinDto);

            coinservice.Setup(coinservice => coinservice.GetAsync(newId)).ReturnsAsync(default(CoinDto));

            coinservice.Setup(coinservice => coinservice.CreateAsync(recivedCoin)).ReturnsAsync(coinDto);

            coinservice.Setup(coinservice => coinservice.CreateMultipleAsync(recivedCoinList)).ReturnsAsync(coinDtoList);

            coinservice.Setup(coinservice => coinservice.UpdateAsync(updateId, recivedCoin)).ReturnsAsync(coinDto);
            coinservice.Setup(coinservice => coinservice.UpdateAsync(symbol, recivedCoin)).ReturnsAsync(coinDto);
            coinservice.Setup(coinservice => coinservice.UpdateAsync(newId, recivedCoin)).ReturnsAsync(default(CoinDto));
            coinservice.Setup(coinservice => coinservice.DeleteAsync(newId)).ReturnsAsync(false);
            coinservice.Setup(coinservice => coinservice.DeleteAsync(coinDto.Id)).ReturnsAsync(true);
        }
        #region GetAsync
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

            coinservice.Verify(coinservice => coinservice.GetAllAsync(), Times.Once());
        }

        [Test]
        public async Task GetAsync_Return_ListOfCoinDto()
        {
            var result = await controller.GetAsync();

            var objresult = (OkObjectResult)result;

            Assert.That(objresult.Value, Is.InstanceOf(typeof(IEnumerable<CoinDto>)));
            Assert.That(objresult.Value, Is.EqualTo(coinDtoList));
        }
        #endregion

        #region GetByIdAsync
        [Test]
        public async Task GetByIdAsync_GetCoinAsync_CalledOneTime()
        {
            var result = await controller.GetByIdAsync(newId);

            coinservice.Verify(coinservice => coinservice.GetAsync(newId), Times.Once());
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

            Assert.That(objresult.Value, Is.EqualTo(coinDto));
        }
        #endregion

        #region PostAsync
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
            Assert.That(objresult.Value, Is.EqualTo(coinDto));
        }


        [Test]
        public async Task PostAsync_Publish_CalledOnce()
        {
            var result = await controller.PostAsync(recivedCoin);
            coinPublisher.Verify(coinservice => coinservice.PublishCreateAsync(coinDto), Times.Once());
        }

        [Test]
        public async Task PostAsync_Publish_GoOnError_CalledNever()
        {
            coinservice.Setup(coinservice => coinservice.CreateAsync(recivedCoin)).ReturnsAsync(nullCoinDto);

            var result = await controller.PostAsync(recivedCoin);
            coinPublisher.Verify(coinservice => coinservice.PublishCreateAsync(coinDto), Times.Never());
        }
        #endregion

        #region BulkAsync
        [Test]
        public async Task BulkAsync_Returns_200Async()
        {

            var result = await controller.BulkAsync(recivedCoinList);

            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task BulkAsync_Returns_CoinDtoTypes()
        {
            var result = await controller.BulkAsync(recivedCoinList);

            var objresult = (OkObjectResult)result;
            Assert.That(objresult.Value, Is.InstanceOf(typeof(IEnumerable<CoinDto>)));
        }

        [Test]
        public async Task BulkAsync_CreateMutiple_CalledOneTime()
        {
            var result = await controller.BulkAsync(recivedCoinList);

            coinservice.Verify(x => x.CreateMultipleAsync(recivedCoinList), Times.Once);
        }

        [Test]
        public async Task BulkAsync_Returns_CoinDtoList()
        {
            var result = await controller.BulkAsync(recivedCoinList);

            var objresult = (OkObjectResult)result;
            Assert.That(objresult.Value, Is.EqualTo(coinDtoList));
        }

        [Test]
        public async Task BulkAsync_Publish_CalledOnce()
        {
            var result = await controller.BulkAsync(recivedCoinList);
            coinPublisher.Verify(coinservice => coinservice.PublishCreateAsync(coinDtoList), Times.Once());
        }

        #endregion

        #region GetBySimbol
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

            coinservice.Verify(x => x.GetAsync(symbol), Times.Once);
        }
        [Test]
        public async Task GetBySimbol_Retruns_CoinDto()
        {
            var result = await controller.GetBySimbolAsync(symbol);

            var objresult = (OkObjectResult)result;
            Assert.That(objresult.Value, Is.EqualTo(coinDto));
        }

        [Test]
        public async Task GetBySimbol_Retruns_404_WhenNotFound()
        {
            coinservice.Setup(x => x.GetAsync(symbol)).ReturnsAsync(default(CoinDto));

            var result = await controller.GetBySimbolAsync(symbol);

            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)));

        }
        #endregion

        #region PutAsync
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

            coinservice.Verify(x => x.UpdateAsync(updateId, recivedCoin), Times.Once);
        }

        [Test]
        public async Task PutAsync_Returns_CoinDto()
        {
            var result = await controller.PutAsync(updateId, recivedCoin);

            var objresult = (OkObjectResult)result;

            Assert.That(objresult.Value, Is.EqualTo(coinDto));
        }
        [Test]
        public async Task PutAsync_CoinNotFund_Returns_404Code()
        {
            var result = await controller.PutAsync(newId, recivedCoin);

            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)));
        }

        [Test]
        public async Task PutAsync_Publish_CalledOnce()
        {
            var result = await controller.PutAsync(updateId, recivedCoin);
            coinPublisher.Verify(coinservice => coinservice.PublishUpdateAsync(coinDto), Times.Once());
        }

        [Test]
        public async Task PutAsync_Publish_GoOnError_CalledNever()
        {

            var result = await controller.PutAsync(newId, recivedCoin);
            coinPublisher.Verify(coinservice => coinservice.PublishUpdateAsync(coinDto), Times.Never());
        }
        #endregion

        #region PutBySymbolAsync
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

            coinservice.Verify(x => x.UpdateAsync(symbol, recivedCoin), Times.Once);
        }

        [Test]
        public async Task PutBySymbolAsync_Returns_CoinDto()
        {
            var result = await controller.PutBySymbolAsync(symbol, recivedCoin);

            var objresult = (OkObjectResult)result;

            Assert.That(objresult.Value, Is.EqualTo(coinDto));
        }
        [Test]
        public async Task PutBySymbolAsync_CoinNotFund_Returns_404Codee()
        {
            coinservice.Setup(coinservice => coinservice.UpdateAsync(symbol, recivedCoin)).ReturnsAsync(default(CoinDto));

            var result = await controller.PutBySymbolAsync(symbol, recivedCoin);

            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)));

        }

        [Test]
        public async Task PutBySymbolAsync_Publish_CalledOnce()
        {
            var result = await controller.PutBySymbolAsync(symbol, recivedCoin);
            coinPublisher.Verify(coinservice => coinservice.PublishUpdateAsync(coinDto), Times.Once());
        }

        [Test]
        public async Task PutBySymbolAsync_Publish_GoOnError_CalledNever()
        {
            coinservice.Setup(coinservice => coinservice.UpdateAsync(symbol, recivedCoin)).ReturnsAsync(nullCoinDto);

            var result = await controller.PutBySymbolAsync(symbol, recivedCoin);
            coinPublisher.Verify(coinservice => coinservice.PublishUpdateAsync(coinDto), Times.Never());
        }

        [Test]
        public async Task Delete_Returns_Returns_200Code()
        {
            var result = await controller.DeleteAsync(coinDto.Id);

            Assert.That(result, Is.TypeOf<OkResult>());
        }
        #endregion

        #region DeleteAsync
        [Test]
        public async Task Delete_Returns_DeleteCoin_CalledOnce()
        {
            var result = await controller.DeleteAsync(coinDto.Id);

            coinservice.Verify(x => x.DeleteAsync(coinDto.Id), Times.Once);
        }

        [Test]
        public async Task Delete_Returns_DeleteCoin_ReturnFalse_Retruns_404Code()
        {
            var result = await controller.DeleteAsync(newId);

            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)));
        }

        [Test]
        public async Task DeleteAsync_Publish_CalledOnce()
        {
            var result = await controller.DeleteAsync(coinDto.Id);
            coinPublisher.Verify(coinservice => coinservice.PublishDeleteAsync(coinDto.Id), Times.Once());
        }

        [Test]
        public async Task DeleteAsync_Publish_GoOnError_CalledNever()
        {
            var result = await controller.DeleteAsync(newId);

            coinPublisher.Verify(coinservice => coinservice.PublishDeleteAsync(newId), Times.Never());
        }
        #endregion
    }
}
