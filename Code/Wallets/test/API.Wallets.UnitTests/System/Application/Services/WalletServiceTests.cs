using CoinTracker.API.SDK.Application.DataMapper;
using CoinTracker.API.SDK.Application.IProvider;
using CoinTracker.API.Wallets.Application.Services;
using CoinTracker.API.Wallets.Domain.Dtos;
using CoinTracker.API.Wallets.Domain.Entities;
using CoinTracker.API.Wallets.UnitTests.Fixtures;
using Moq;

namespace CoinTracker.API.Wallets.UnitTests.System.Application.Services
{
    internal class WalletServiceTests
    {
        private RecivedWalletDto recivedWallet;
        private WalletDto walletDto;
        private IEnumerable<WalletDto> walletDtoList;
        private Wallet walletEntity;
        private IEnumerable<Wallet> walletEntityList;
        private Guid walletDtoId;
        private Mock<IProvider<Wallet>> provider;
        private Mock<IDataMapper> mapper;
        private WalletService walletService;

        [SetUp]
        public void Setup()
        {
            recivedWallet = WalletFixture.RecivedWalletDto();
            walletDto = WalletFixture.WalletDto();
            walletDtoList = WalletFixture.WalletDtoList();
            walletEntity = WalletFixture.Wallet();
            walletEntityList = WalletFixture.WalletList();

            walletDtoId = walletDto.Id;

            provider = new Mock<IProvider<Wallet>>();
            mapper = new Mock<IDataMapper>();

            provider.Setup(provider => provider.CreateAsync(It.IsAny<Wallet>())).ReturnsAsync(walletEntity);
            provider.Setup(provider => provider.GetAsync(walletDtoId)).ReturnsAsync(walletEntity);
            provider.Setup(provider => provider.DeleteAsync(walletDtoId)).ReturnsAsync(true);
            provider.Setup(provider => provider.GetAllAsync()).ReturnsAsync(walletEntityList);

            mapper.Setup(mapper => mapper.Map<Wallet>(recivedWallet)).Returns(walletEntity);
            mapper.Setup(mapper => mapper.Map<WalletDto>(walletEntity)).Returns(walletDto);
            mapper.Setup(mapper => mapper.Map<IEnumerable<WalletDto>>(walletEntityList)).Returns(walletDtoList);


            walletService = new WalletService(provider.Object, mapper.Object);
        }
        [Test]
        public async Task CreateAsync_Retrurn_WalletDto()
        {
            var actualWallet = await walletService.CreateAsync(recivedWallet);

            Assert.AreEqual(actualWallet, walletDto);
        }

        [Test]
        public async Task CreateAsync_IProvider_CalledOnes()
        {
            var actualWallet = await walletService.CreateAsync(recivedWallet);

            provider.Verify(provider => provider.CreateAsync(It.IsAny<Wallet>()), Times.Once);
        }

        [Test]
        public async Task GetWalletAsync_IProvider_CalledOnes()
        {
            var actualWallet = await walletService.GetAsync(walletDtoId);

            provider.Verify(provider => provider.GetAsync(walletDtoId), Times.Once);
        }

        [Test]
        public async Task GetWalletAsync_Return_WalletDto()
        {
            var actualWallet = await walletService.GetAsync(walletDtoId);

            Assert.AreEqual(actualWallet, walletDto);
        }

        [Test]
        public async Task DeleteWalletAsync_DeleteAsync_CalledOnce()
        {
            var ret = await walletService.DeleteAsync(walletDtoId);

            provider.Verify(provider => provider.DeleteAsync(walletDtoId), Times.Once);
        }

        [Test]
        public async Task DeleteWalletAsync_ReturnTrue()
        {
            var ret = await walletService.DeleteAsync(walletDtoId);

            Assert.IsTrue(ret);
        }

        [Test]
        public async Task DeleteWalletAsync_DeleteAsyncFalse_ReturnFalse()
        {
            provider.Setup(provider => provider.DeleteAsync(walletDtoId)).ReturnsAsync(false);

            var ret = await walletService.DeleteAsync(walletDtoId);

            Assert.IsFalse(ret);
        }

        [Test]
        public async Task GetWalletAsync_GetAllAsync_CalledOnce()
        {

            var ret = await walletService.GetAllAsync();

            provider.Verify(provider => provider.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetWalletAsync_Return_WalletDotIEnumerable()
        {
            var wallets = await walletService.GetAllAsync();

            Assert.AreEqual(wallets, walletDtoList);
        }
    }
}
