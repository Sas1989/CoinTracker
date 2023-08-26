using API.CoinList.Domain.Dtos;
using API.CoinList.Domain.Entities;
using API.CoinList.Infrastructure.Services;
using API.SDK.Application.DataMapper;
using API.SDK.Application.Repository;

namespace API.CoinList.UnitTests.System.Infrastructure.Services
{
    internal class CoinServiceTests
    {
        private Mock<IRepository<Coin>> provider;
        private Mock<IDataMapper> mapper;
        private CoinService coinService;
        private CoinDtoInput recivedCoin;
        private CoinDto coinDto;
        private Coin coin;
        private string symbolCoin;
        private IEnumerable<Coin> coinList;
        private IEnumerable<CoinDto> coinDtoList;
        private IEnumerable<CoinDtoInput> recivedCoinList;
        private Guid idNew;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<IRepository<Coin>>();
            mapper = new Mock<IDataMapper>();

            coinService = new CoinService(provider.Object, mapper.Object);

            coinList = FixureManger.CreateList<Coin>();
            coinDtoList = FixureManger.CreateList<CoinDto>();
            recivedCoinList = FixureManger.CreateList<CoinDtoInput>();

            recivedCoin = recivedCoinList.First();
            coinDto = coinDtoList.First();
            coin = coinList.First();

            idNew = Guid.NewGuid();

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
        public void GetCoinAsync_EmptySymbol_ThrowException()
        {
            var func = () => coinService.GetAsync(string.Empty);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("symbol"));
        }

        [Test]
        public void GetCoinAsync_NullSymbol_ThrowException()
        {
            var func = () => coinService.GetAsync(null!);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("symbol"));
        }

        [Test]
        public async Task GetCoinAsync_GetAsync_CalledOnce()
        {
            await coinService.GetAsync(symbolCoin);

            provider.Verify(x => x.GetAsync(nameof(Coin.Symbol), symbolCoin), Times.Once);
        }

        [Test]
        public async Task GetCoinAsync_CoinNotFound_ReturnDefault()
        {
            provider.Setup(provider => provider.GetAsync(nameof(Coin.Symbol), symbolCoin)).ReturnsAsync(Enumerable.Empty<Coin>());
            var result = await coinService.GetAsync(symbolCoin);

            Assert.That(result,Is.EqualTo(default(CoinDto)));

        }

        [Test]
        public async Task GetCoinAsync_Return_CoinDto()
        {
            var result = await coinService.GetAsync(symbolCoin);

            Assert.That(result, Is.EqualTo(coinDto));

        }

        [Test]
        public void UpdateCoinSymbol_EmptySymbol_ThrowException()
        {
            var func = () => coinService.UpdateAsync(string.Empty, recivedCoin);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
        }

        [Test]
        public void UpdateCoinSymbol_NullSymbol_ThrowException()
        {
            var func = () => coinService.UpdateAsync(null!, recivedCoin);

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

            var result = await coinService.UpdateAsync(symbolCoin, recivedCoin);

            Assert.That(result, Is.EqualTo(default(CoinDto)));

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
