using API.Contracts.Coin;
using API.SDK.Application.DataMapper;
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
    internal class CoinInsertConsumerTests
    {
        private Mock<ICoinService> coinService;
        private Mock<IDataMapper> dataMapper;
        private CoinInserConsumer consumer;
        private Mock<ConsumeContext<CoinInsert>> coinMessage;
        private CoinInsert coinContract;
        private CoinDto coinDto;

        [SetUp]
        public void SetUp()
        {
            coinService = new Mock<ICoinService>();
            dataMapper = new Mock<IDataMapper>();
            consumer = new CoinInserConsumer(coinService.Object, dataMapper.Object);

            coinMessage = new Mock<ConsumeContext<CoinInsert>>();
            coinContract = CoinFixture.CoinInsert();
            coinDto = CoinFixture.CoinDto();

            coinMessage.Setup(coinMessage => coinMessage.Message).Returns(coinContract);
            dataMapper.Setup(dataMapper => dataMapper.Map<CoinDto>(coinContract)).Returns(coinDto);

        }

        [Test]
        public async Task Consume_CalledCreate_OnesAsync()
        {
            await consumer.Consume(coinMessage.Object);

            coinService.Verify(coinService => coinService.CreateAsync(coinDto), Times.Once);
        }

        [Test]
        public async Task Consume_CalledDataMap_OnesAsync()
        {
            await consumer.Consume(coinMessage.Object);

            dataMapper.Verify(dataMapper => dataMapper.Map<CoinDto>(coinContract), Times.Once);
        }
    }
}
