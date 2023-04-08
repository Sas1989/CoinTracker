using CoinTracker.Api.CoinList.Infrastructure.Publishers;
using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.UnitTests.Fixture;
using CoinTracker.API.Contracts.Coin;
using CoinTracker.API.SDK.Application.DataMapper;
using MassTransit;
using Moq;

namespace CoinTracker.API.CoinList.UnitTests.System.Infrastructure
{
    internal class RabbitMqPublisherTests
    {
        private Mock<IPublishEndpoint> publishEndPoint;
        private Mock<IDataMapper> mapper;
        private RabbitMqPublisher rabbitMqPublish;
        private CoinDto coinDto;
        private IEnumerable<CoinDto> coinDtoList;
        private CoinUpdate coinInsert;
        private IEnumerable<CoinUpdate> coinInsertList;
        private CoinUpdate coinUpdate;
        private CoinDelete coinDelete;

        [SetUp]
        public void Setup()
        {
            publishEndPoint = new Mock<IPublishEndpoint>();
            mapper = new Mock<IDataMapper>();
            rabbitMqPublish = new RabbitMqPublisher(publishEndPoint.Object, mapper.Object);
            

            coinDto = CoinFixture.GenerateCoinDtos();
            coinDtoList = CoinFixture.GenereteListOfCoinDtos();
            
            coinInsert = CoinFixture.GenerateCoinInsert();
            coinInsertList = CoinFixture.GenereteListOfCoinInsert();
            coinUpdate = CoinFixture.GenerateCoinUpdate();
            coinDelete = CoinFixture.GenerateCoinDelete();

            mapper.Setup(mapper => mapper.Map<CoinUpdate>(coinDto)).Returns(coinInsert);
            mapper.Setup(mapper => mapper.Map<IEnumerable<CoinUpdate>>(coinDto)).Returns(coinInsertList);
            mapper.Setup(mapper => mapper.Map<CoinUpdate>(coinDto)).Returns(coinUpdate);

        }

        [Test]
        public async Task PublishCreateAsync_MapCoinDto_CoinContract()
        {
            rabbitMqPublish.PublishCreateAsync(coinDto);

            mapper.Verify(mapper => mapper.Map<CoinUpdate>(coinDto), Times.Once);
        }

        [Test]
        public async Task PublishCreateAsync_Publish_Called_Once()
        {
            rabbitMqPublish.PublishCreateAsync(coinDto);

            publishEndPoint.Verify(publishEndPoint => publishEndPoint.Publish(coinInsert,default), Times.Once);

        }

        [Test]
        public async Task PublishUpdateAsync_MapCoinDto_CoinContract()
        {
            rabbitMqPublish.PublishUpdateAsync(coinDto);

            mapper.Verify(mapper => mapper.Map<CoinUpdate>(coinDto), Times.Once);
        }

        [Test]
        public async Task PublishUpdateAsync_Publish_Called_Once()
        {
            rabbitMqPublish.PublishUpdateAsync(coinDto);

            publishEndPoint.Verify(publishEndPoint => publishEndPoint.Publish(coinUpdate, default), Times.Once);

        }

        [Test]
        public async Task PublishDeleteAsync_Publish_Called_Once()
        {
            rabbitMqPublish.PublishDeleteAsync(coinDto.Id);

            publishEndPoint.Verify(publishEndPoint => publishEndPoint.Publish(It.IsAny<CoinDelete>(), default), Times.Once);
        }
        [Test]
        public async Task PublishCreateAsync_Massive_MapCoinDto_CoinContract()
        {
            rabbitMqPublish.PublishCreateAsync(coinDtoList);

            mapper.Verify(mapper => mapper.Map<IEnumerable<CoinUpdate>>(coinDtoList), Times.Once);
        }

    }
}
