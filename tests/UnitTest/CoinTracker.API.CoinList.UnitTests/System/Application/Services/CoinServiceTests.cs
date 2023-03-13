using CoinTracker.API.CoinList.Application.Services;
using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.Domain.Entities;
using CoinTracker.API.CoinList.UnitTests.Fixture;
using CoinTracker.API.SDK.Application.DataMapper;
using CoinTracker.API.SDK.Application.IProvider;
using CoinTracker.API.SDK.UnitTests.System.Application.ApplicationService;
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
        public async Task GetCoinAsync_EmptySymbol_ThrowException()
        {
            var func = () => coinService.GetAsync(String.Empty);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("symbol"));
        }

        [Test]
        public async Task GetCoinAsync_NullSymbol_ThrowException()
        {
            var func = () => coinService.GetAsync((string)null);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("symbol"));
        }

        [Test]
        public async Task GetCoinAsync_GetAsync_CalledOnce()
        {
            coinService.GetAsync(symbolCoin);

            provider.Verify(x => x.GetAsync(nameof(Coin.Symbol), symbolCoin), Times.Once);
        }

        [Test]
        public async Task GetCoinAsync_CoinNotFound_ThrowException()
        {
            provider.Setup(provider => provider.GetAsync(nameof(Coin.Symbol), symbolCoin)).ReturnsAsync(Enumerable.Empty<Coin>());
            var func = () => coinService.GetAsync(symbolCoin);

            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => func());

        }

        [Test]
        public async Task GetCoinAsync_Return_CoinDto()
        {
            var result = await coinService.GetAsync(symbolCoin);

            Assert.That(result, Is.EqualTo(coinDto));

        }

        [Test]
        public async Task UpdateCoinSymbol_EmptySymbol_ThrowException()
        {
            var func = () => coinService.UpdateAsync(string.Empty, recivedCoin);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
        }

        [Test]
        public async Task UpdateCoinSymbol_NullSymbol_ThrowException()
        {
            var func = () => coinService.UpdateAsync((string)null, recivedCoin);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
        }

        [Test]
        public async Task UpdateCoinSymbol_GetAsync_CalledOnce()
        {
            var result = await coinService.UpdateAsync(symbolCoin, recivedCoin);

            provider.Verify(provider => provider.GetAsync(nameof(Coin.Symbol), symbolCoin), Times.Once);
        }

        [Test]
        public async Task UpdateCoinSymbol_CoinNotFound_ThrowException()
        {
            provider.Setup(provider => provider.GetAsync(nameof(Coin.Symbol), symbolCoin)).ReturnsAsync(Enumerable.Empty<Coin>());

            var func = () => coinService.UpdateAsync(symbolCoin, recivedCoin);

            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => func());

        }

        [Test]
        public async Task UpdateCoinSymbol_UpdateAsync_CalledOnce()
        {
            var result = await coinService.UpdateAsync(symbolCoin, recivedCoin);

            provider.Verify(provider => provider.UpdateAsync(coin), Times.Once);
        }

        [Test]
        public async Task UpdateCoinSymbol_ReturnCoinDto()
        {
            var result = await coinService.UpdateAsync(symbolCoin, recivedCoin);

            Assert.That(result, Is.EqualTo(coinDto));
        }
    }
}
