﻿using API.Contracts.Coin;
using API.Wallets.Application.Services;
using API.Wallets.Consumers;
using API.Wallets.Domain.Dtos.Coin;
using MassTransit;

namespace API.Wallets.UnitTests.System.Consumers
{
    internal class CoinDeleteConsumerTests
    {
        private Mock<ICoinService> coinService;
        private CoinDeleteConsumer consumer;
        private Mock<ConsumeContext<CoinDelete>> coinMessage;
        private CoinDelete coinContract;
        private CoinDto coinDto;

        [SetUp]
        public void SetUp()
        {
            coinService = new Mock<ICoinService>();
            consumer = new CoinDeleteConsumer(coinService.Object);

            coinMessage = new Mock<ConsumeContext<CoinDelete>>();
            coinContract = FixureManger.Create<CoinDelete>();
            coinDto = FixureManger.Create<CoinDto>();

            coinMessage.Setup(coinMessage => coinMessage.Message).Returns(coinContract);

        }

        [Test]
        public async Task Consume_CalledDelete_OnesAsync()
        {
            await consumer.Consume(coinMessage.Object);

            coinService.Verify(coinService => coinService.DeleteAsync(coinContract.Id), Times.Once);
        }
    }
}