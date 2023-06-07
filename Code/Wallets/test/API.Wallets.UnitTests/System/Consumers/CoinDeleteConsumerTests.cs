using API.Contracts.Coin;
using API.Wallets.Application.Services.Interfaces;
using API.Wallets.Consumers;
using API.Wallets.Domain.Dtos.Coin;
using API.Wallets.UnitTests.Fixtures;
using MassTransit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            coinContract = CoinFixture.CoinDelete();
            coinDto = CoinFixture.CoinDto();

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
