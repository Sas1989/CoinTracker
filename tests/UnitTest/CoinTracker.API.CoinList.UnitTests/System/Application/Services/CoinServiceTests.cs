using CoinTracker.API.CoinList.Application.Services;
using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.Domain.Entities;
using CoinTracker.API.CoinList.UnitTests.Fixture;
using CoinTracker.API.SDK.Application.DataMapper;
using CoinTracker.API.SDK.Application.IProvider;
using Moq;

namespace CoinTracker.API.CoinList.UnitTests.System.Application.Services
{
    internal class CoinServiceTests
    {
        private Mock<IProvider<Coin>> provider;
        private Mock<IDataMapper> mapper;
        private CoinService coinService;
        private RecivedCoinDto recivedCoin;
        private CoinDto coinDto;
        private Coin coin;
        private string symbolCoin;
        private Coin coinNull;
        private IEnumerable<Coin> coinList;
        private IEnumerable<CoinDto> coinDtoList;
        private IEnumerable<RecivedCoinDto> recivedCoinList;
        private Guid idNew;
        private Guid updateId;
        private Guid idEmpty;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<IProvider<Coin>>();
            mapper = new Mock<IDataMapper>();

            coinService = new CoinService(provider.Object, mapper.Object);

            coinList = CoinFixture.GenereteListOfCoin();
            coinDtoList = CoinFixture.GenereteListOfCoinDtos();
            recivedCoinList = CoinFixture.GenereteListOfRecivedDtos();


            recivedCoin = recivedCoinList.First();
            coinDto = coinDtoList.First();
            coin = coinList.First();

            idNew = Guid.NewGuid();
            updateId = coin.Id;
            idEmpty = Guid.Empty;

            symbolCoin = coin.Symbol;

            mapper.Setup(mapper => mapper.Map<CoinDto>(coin)).Returns(coinDto);
            mapper.Setup(mapper => mapper.Map<Coin>(recivedCoin)).Returns(coin);
            mapper.Setup(mapper => mapper.Map<IEnumerable<CoinDto>>(coinList)).Returns(coinDtoList);
            mapper.Setup(mapper => mapper.Map<IEnumerable<Coin>>(recivedCoinList)).Returns(coinList);

            provider.Setup(provider => provider.CreateAsync(coin)).ReturnsAsync(coin);
            provider.Setup(provider => provider.CreateAsync(coinList)).ReturnsAsync(coinList);
            provider.Setup(provider => provider.GetAllAsync()).ReturnsAsync(coinList);
            provider.Setup(provider => provider.GetAsync(idNew)).ReturnsAsync(coin);
            provider.Setup(provider => provider.GetAsync(nameof(Coin.Symbol), symbolCoin)).ReturnsAsync(coinList);
            provider.Setup(provider => provider.UpdateAsync(coin)).ReturnsAsync(coin);
            provider.Setup(provider => provider.DeleteAsync(coin.Id)).ReturnsAsync(true);


        }

        [Test]
        public void CreateAsync_CreateAsync_CalledOnce()
        {
            coinService.CreateAsync(recivedCoin);

            provider.Verify(provider => provider.CreateAsync(It.IsAny<Coin>()), Times.Once());
        }

        [Test]
        public void CreateAsync_Mapped_CalledOnce()
        {

            coinService.CreateAsync(recivedCoin);

            mapper.Verify(mapper => mapper.Map<Coin>(recivedCoin), Times.Once());
        }

        [Test]
        public async Task CreateAsync_CoinDto_Returned()
        {

            var result = await coinService.CreateAsync(recivedCoin);

            Assert.That(result, Is.TypeOf(typeof(CoinDto)));
        }

        [Test]
        public async Task CreateAsync_CoinDto_ReturnedCorrect()
        {

            var result = await coinService.CreateAsync(recivedCoin);

            Assert.That(result, Is.EqualTo(coinDto));
        }

        [Test]
        public async Task GetAllCoinsAsync_GetAllAsync_CalledOnce()
        {
            await coinService.GetAllCoinsAsync();

            provider.Verify(provider => provider.GetAllAsync(), Times.Once);
        }
        [Test]
        public async Task GetAllCoinsAsync_Retrun_IEnumberableCoinDto()
        {
            var result = await coinService.GetAllCoinsAsync();

            Assert.That(result, Is.InstanceOf(typeof(IEnumerable<CoinDto>)));
        }

        [Test]
        public async Task GetAllCoinsAsync_Map_CalledOne()
        {
            await coinService.GetAllCoinsAsync();

            mapper.Verify(mapper => mapper.Map<IEnumerable<CoinDto>>(coinList), Times.Once);
        }

        [Test]
        public async Task GetAllCoinsAsync_CoinDto_ReturnedCorrect()
        {
            var result = await coinService.GetAllCoinsAsync();

            Assert.That(result, Is.EqualTo(coinDtoList));

        }

        [Test]
        public void GetCoinAsync_IdEmpty_ThrowException()
        {
            var func = () => coinService.GetCoinAsync(idEmpty);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public async Task GetCoinAsync_CoinDto_TypeReturned()
        {
            var result = await coinService.GetCoinAsync(idNew);

            Assert.That(result, Is.TypeOf(typeof(CoinDto)));
        }

        [Test]
        public async Task GetCoinAync_GetAsync_CalledOnce()
        {

            var result = await coinService.GetCoinAsync(idNew);

            provider.Verify(provider => provider.GetAsync(idNew), Times.Once);
        }

        [Test]
        public async Task GetCoinAync_CoinNotFound_ReturnNull()
        {
            provider.Setup(provider => provider.GetAsync(idNew)).ReturnsAsync(coinNull);

            var result = await coinService.GetCoinAsync(idNew);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetCoinAync_CoinDto_ReturnedCorrect()
        {
            var result = await coinService.GetCoinAsync(idNew);

            Assert.That(result, Is.EqualTo(coinDto));
        }

        [Test]
        public async Task CreateMultipleAsync_CoinDot_TypeReturn()
        {
            var result = await coinService.CreateMultipleAsync(recivedCoinList);

            Assert.That(result, Is.InstanceOf(typeof(IEnumerable<CoinDto>)));
        }

        [Test]
        public async Task CreateMultipleAsync_CreateAsync_CalledOnce()
        {
            var result = await coinService.CreateMultipleAsync(recivedCoinList);

            provider.Verify(x => x.CreateAsync(coinList), Times.Once);
        }

        [Test]
        public async Task CreateMultipleAsync_Returns_Corrects()
        {
            var result = await coinService.CreateMultipleAsync(recivedCoinList);

            Assert.AreEqual(coinDtoList, result);

        }
        [Test]
        public async Task GetCoinAsync_EmptySymbol_ThrowException()
        {
            var func = () => coinService.GetCoinAsync(String.Empty);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("symbol"));
        }

        [Test]
        public async Task GetCoinAsync_NullSymbol_ThrowException()
        {
            var func = () => coinService.GetCoinAsync((string)null);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("symbol"));
        }

        [Test]
        public async Task GetCoinAsync_GetAsync_CalledOnce()
        {
            coinService.GetCoinAsync(symbolCoin);

            provider.Verify(x => x.GetAsync(nameof(Coin.Symbol), symbolCoin), Times.Once);
        }

        [Test]
        public async Task GetCoinAsync_GetAsync_ReturnEmptyList()
        {
            provider.Setup(provider => provider.GetAsync(nameof(Coin.Symbol), symbolCoin)).ReturnsAsync(Enumerable.Empty<Coin>());
            var result = await coinService.GetCoinAsync(symbolCoin);

            Assert.IsNull(result);

        }

        [Test]
        public async Task GetCoinAsync_Return_CoinDto()
        {
            var result = await coinService.GetCoinAsync(symbolCoin);

            Assert.That(result, Is.EqualTo(coinDto));

        }

        [Test]
        public async Task UpdateCoin_EmptyGuid_ThrowException()
        {
            var func = () => coinService.UpdateCoin(Guid.Empty, recivedCoin);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public async Task UpdateCoin_Mapper_CalledOnce()
        {
            coinService.UpdateCoin(updateId, recivedCoin);

            mapper.Verify(mapper => mapper.Map<Coin>(recivedCoin), Times.Once);
        }

        [Test]
        public async Task UpdateCoin_Update_CalledOnce()
        {
            coinService.UpdateCoin(updateId, recivedCoin);

            provider.Verify(provider => provider.UpdateAsync(coin), Times.Once);
        }

        [Test]
        public async Task UpdateCoin_Retruns_CoinDto()
        {
            var result = await coinService.UpdateCoin(updateId, recivedCoin);

            Assert.AreEqual(coinDto, result);

        }

        [Test]
        public async Task UpdateCoinSymbol_EmptySymbol_ThrowException()
        {
            var func = () => coinService.UpdateCoin(string.Empty, recivedCoin);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
        }

        [Test]
        public async Task UpdateCoinSymbol_NullSymbol_ThrowException()
        {
            var func = () => coinService.UpdateCoin((string)null, recivedCoin);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
        }

        [Test]
        public async Task UpdateCoinSymbol_GetAsync_CalledOnce()
        {
            var result = await coinService.UpdateCoin(symbolCoin, recivedCoin);

            provider.Verify(provider => provider.GetAsync(nameof(Coin.Symbol), symbolCoin), Times.Once);
        }

        [Test]
        public async Task UpdateCoinSymbol_GetAsync_ReturnNull_ReturnNull()
        {
            provider.Setup(provider => provider.GetAsync(nameof(Coin.Symbol), symbolCoin)).ReturnsAsync(Enumerable.Empty<Coin>());

            var result = await coinService.UpdateCoin(symbolCoin, recivedCoin);

            Assert.IsNull(result);

        }

        [Test]
        public async Task UpdateCoinSymbol_UpdateAsync_CalledOnce()
        {
            var result = await coinService.UpdateCoin(symbolCoin, recivedCoin);

            provider.Verify(provider => provider.UpdateAsync(coin), Times.Once);
        }

        [Test]
        public async Task UpdateCoinSymbol_ReturnCoinDto()
        {
            var result = await coinService.UpdateCoin(symbolCoin, recivedCoin);

            Assert.That(result, Is.EqualTo(coinDto));
        }

        [Test]
        public async Task DeleteCoin_ReturnBool()
        {
            var result = await coinService.DeleteCoin(coin.Id);

            Assert.That(result, Is.TypeOf(typeof(bool)));
        }

        [Test]
        public async Task DeleteCoin_DeleteAsync_CalledOnce()
        {
            var result = await coinService.DeleteCoin(coin.Id);

            provider.Verify(provider => provider.DeleteAsync(coin.Id), Times.Once);
        }

        [Test]
        public async Task DeleteCoin_EmptyId_ReturnFalse()
        {
            var result = await coinService.DeleteCoin(idEmpty);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteCoin_DeleteAsync_ReturnFalse_ReturnFalse()
        {
            provider.Setup(provider => provider.DeleteAsync(coin.Id)).ReturnsAsync(false);

            var result = await coinService.DeleteCoin(coin.Id);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteCoin_DeleteAsync_ReturnTrue_ReturnTrue()
        {
            var result = await coinService.DeleteCoin(coin.Id);

            Assert.That(result, Is.True);
        }
    }
}
