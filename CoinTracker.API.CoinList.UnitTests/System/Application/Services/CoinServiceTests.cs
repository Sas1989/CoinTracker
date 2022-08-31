using CoinTracker.API.CoinList.Application.Common.Mappers;
using CoinTracker.API.CoinList.Application.Providers;
using CoinTracker.API.CoinList.Application.Services;
using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.Domain.Entities;
using CoinTracker.API.CoinList.UnitTests.Fixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.UnitTests.System.Application.Services
{
    internal class CoinServiceTests
    {
        private Mock<IProvider> provider;
        private Mock<IDataMapper> mapper;
        private CoinService coinService;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<IProvider>();
            mapper = new Mock<IDataMapper>();
            coinService = new CoinService(provider.Object, mapper.Object);
        }

        [Test]
        public void CreateAsync_CreateAsync_CalledOnce()
        {
            RecivedCoinDto recivedCoin = CoinFixture.GenerateRecivedDtos();

            coinService.CreateAsync(recivedCoin);

            provider.Verify(provider => provider.CreateAsync(It.IsAny<Coin>()), Times.Once());
        }

        [Test]
        public void CreateAsync_Mapped_CalledOnce()
        {

            RecivedCoinDto recivedCoin = CoinFixture.GenerateRecivedDtos();

            coinService.CreateAsync(recivedCoin);

            mapper.Verify(mapper => mapper.Map<Coin>(recivedCoin), Times.Once());
        }

        [Test]
        public async Task CreateAsync_CoinDto_Returned()
        {

            RecivedCoinDto recivedCoin = CoinFixture.GenerateRecivedDtos();
            CoinDto coinDto = CoinFixture.GenerateCoinDtos();

            mapper.Setup(mapper => mapper.Map<CoinDto>(It.IsAny<Coin>())).Returns(coinDto);

            var result = await coinService.CreateAsync(recivedCoin);

            Assert.That(result, Is.TypeOf(typeof(CoinDto)));

        }

        [Test]
        public async Task CreateAsync_CoinDto_ReturnedCorrect()
        {

            RecivedCoinDto recivedCoin = CoinFixture.GenerateRecivedDtos();
            Coin coin = CoinFixture.GenerateCoin();
            CoinDto coinDto = CoinFixture.GenerateCoinDtos();

            mapper.Setup(mapper => mapper.Map<Coin>(recivedCoin)).Returns(coin);
            provider.Setup(provider => provider.CreateAsync(coin)).ReturnsAsync(coin);
            mapper.Setup(mapper => mapper.Map<CoinDto>(coin)).Returns(coinDto);

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
            IEnumerable<Coin> coin = CoinFixture.GenereteListOfCoin();
            provider.Setup(provider => provider.GetAllAsync()).ReturnsAsync(coin);

            await coinService.GetAllCoinsAsync();

            mapper.Verify(mapper => mapper.Map<IEnumerable<CoinDto>>(coin), Times.Once);
        }

        [Test]
        public async Task GetAllCoinsAsync_CoinDto_ReturnedCorrect()
        {
            IEnumerable<Coin> coin = CoinFixture.GenereteListOfCoin();
            IEnumerable<CoinDto> coinDto = CoinFixture.GenereteListOfCoinDtos();

            provider.Setup(provider => provider.GetAllAsync()).ReturnsAsync(coin);
            mapper.Setup(mapper => mapper.Map<IEnumerable<CoinDto>>(coin)).Returns(coinDto);

            var result = await coinService.GetAllCoinsAsync();

            Assert.That(result, Is.EqualTo(coinDto));

        }
        [Test]
        public void GetCoinAsync_IdEmpty_ThrowException()
        {
            Guid id = Guid.Empty;

            var func = () => coinService.GetCoinAsync(id);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public async Task GetCoinAsync_CoinDto_TypeReturned()
        {
            Guid id = Guid.NewGuid();
            CoinDto coinDto = CoinFixture.GenerateCoinDtos();

            mapper.Setup(mapper => mapper.Map<CoinDto>(It.IsAny<Coin>())).Returns(coinDto);

            var result = await coinService.GetCoinAsync(id);

            Assert.That(result, Is.TypeOf(typeof(CoinDto)));
        }
        [Test]
        public async Task GetCoinAync_GetAsync_CalledOnce()
        {
            Guid id = Guid.NewGuid();

            var result = await coinService.GetCoinAsync(id);

            provider.Verify(provider => provider.GetAsync(id), Times.Once);
        }

        [Test]
        public async Task GetCoinAync_CoinNotFound_ReturnNull()
        {
            Guid id = Guid.NewGuid();
            Coin coin = null;

            provider.Setup(provider => provider.GetAsync(id)).ReturnsAsync(coin);

            var result = await coinService.GetCoinAsync(id);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetCoinAync_CoinDto_ReturnedCorrect()
        {
            Coin coin = CoinFixture.GenerateCoin();
            Guid id = coin.Id;
            CoinDto coinDto = CoinFixture.GenerateCoinDtos();

            provider.Setup(provider => provider.GetAsync(id)).ReturnsAsync(coin);
            mapper.Setup(mapper => mapper.Map<CoinDto>(coin)).Returns(coinDto);

            var result = await coinService.GetCoinAsync(id);

            Assert.That(result, Is.EqualTo(coinDto));

        }

    }
}
