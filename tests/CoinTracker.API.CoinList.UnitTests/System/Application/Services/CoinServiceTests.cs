﻿using CoinTracker.API.CoinList.Application.Common.Mappers;
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
        private RecivedCoinDto recivedCoin;
        private CoinDto coinDto;
        private Coin coin;
        private string symbolCoin;
        private Coin coinNull;
        private IEnumerable<Coin> coinList;
        private IEnumerable<CoinDto> coinDtoList;
        private IEnumerable<RecivedCoinDto> recivedCoinList;
        private Guid idNew;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<IProvider>();
            mapper = new Mock<IDataMapper>();

            coinService = new CoinService(provider.Object, mapper.Object);

            recivedCoin = CoinFixture.GenerateRecivedDtos();
            coinDto = CoinFixture.GenerateCoinDtos();
            coin = CoinFixture.GenerateCoin();

            symbolCoin = coin.Symbol;

            coinList = CoinFixture.GenereteListOfCoin();
            coinDtoList = CoinFixture.GenereteListOfCoinDtos();
            recivedCoinList = CoinFixture.GenereteListOfRecivedDtos();

            idNew = Guid.NewGuid();

            mapper.Setup(mapper => mapper.Map<CoinDto>(coin)).Returns(coinDto);
            mapper.Setup(mapper => mapper.Map<Coin>(recivedCoin)).Returns(coin);
            mapper.Setup(mapper => mapper.Map<IEnumerable<CoinDto>>(coinList)).Returns(coinDtoList);
            mapper.Setup(mapper => mapper.Map<IEnumerable<Coin>>(recivedCoinList)).Returns(coinList);

            provider.Setup(provider => provider.CreateAsync(coin)).ReturnsAsync(coin);
            provider.Setup(provider => provider.CreateAsync(coinList)).ReturnsAsync(coinList);
            provider.Setup(provider => provider.GetAllAsync()).ReturnsAsync(coinList);
            provider.Setup(provider => provider.GetAsync(idNew)).ReturnsAsync(coin);
            provider.Setup(provider => provider.GetAsync(nameof(Coin.Symbol), symbolCoin)).ReturnsAsync(coinList);

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
            Guid idEmpty = Guid.Empty;

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
    }
}
