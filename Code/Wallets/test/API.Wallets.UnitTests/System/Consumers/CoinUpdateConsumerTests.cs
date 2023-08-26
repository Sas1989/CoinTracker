namespace API.Wallets.UnitTests.System.Consumers
{
    internal class CoinUpdateConsumerTests
    {
 /*       private Mock<ICoinService> coinService;
        private Mock<IDataMapper> dataMapper;
        private CoinUpdateConsumer consumer;
        private Mock<ConsumeContext<CoinUpdate>> coinMessage;
        private CoinUpdate coinContract;
        private CoinDto coinDto;

        [SetUp]
        public void SetUp()
        {
            coinService = new Mock<ICoinService>();
            dataMapper = new Mock<IDataMapper>();
            consumer = new CoinUpdateConsumer(coinService.Object, dataMapper.Object);

            coinMessage = new Mock<ConsumeContext<CoinUpdate>>();
            coinContract = FixureManger.Create<CoinUpdate>();
            coinDto = FixureManger.Create<CoinDto>();

            coinMessage.Setup(coinMessage => coinMessage.Message).Returns(coinContract);
            dataMapper.Setup(dataMapper => dataMapper.Map<CoinDto>(coinContract)).Returns(coinDto);

        }
        [Test]
        public async Task Consume_CalledUpdate_OnesAsync()
        {
            await consumer.Consume(coinMessage.Object);

            coinService.Verify(coinService => coinService.UpdateAsync(coinDto.Id, coinDto), Times.Once);
        }

        [Test]
        public async Task Consume_CalledDataMap_OnesAsync()
        {
            await consumer.Consume(coinMessage.Object);

            dataMapper.Verify(dataMapper => dataMapper.Map<CoinDto>(coinContract), Times.Once);
        }*/
    }
}
