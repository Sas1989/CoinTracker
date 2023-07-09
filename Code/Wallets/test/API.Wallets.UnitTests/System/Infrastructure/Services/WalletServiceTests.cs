using API.SDK.Application.DataMapper;
using API.SDK.Application.Repository;
using API.Wallets.Domain.Dtos.Wallet;
using API.Wallets.Domain.Entities.Wallet;
using API.Wallets.Infrastructure.Services;
using WalletCoin = API.Wallets.Domain.Entities.Wallet.WalletCoin;

namespace API.Wallets.UnitTests.System.Infrastructure.Services
{
    internal class WalletServiceTests
    {
        private WalletDtoInput recivedWallet;
        private WalletDto walletDto;
        private IEnumerable<WalletDto> walletDtoList;
        private Wallet walletEntity;
        private IEnumerable<Wallet> walletEntityList;
        private WalletCoinDtoInput walletCoinDtoInput;
        private WalletCoinDto walletCoinDto;
        private WalletCoin walletCoin;
        private Guid walletId;
        private Guid notExistingWallet;
        private Mock<IRepository<Wallet>> provider;
        private Mock<IDataMapper> mapper;
        private WalletService walletService;

        [SetUp]
        public void Setup()
        {
            recivedWallet = FixureManger.Create<WalletDtoInput>();
            walletDto = FixureManger.Create<WalletDto>();
            walletDtoList = FixureManger.CreateList<WalletDto>();
            walletEntity = FixureManger.Create<Wallet>();
            walletEntityList = FixureManger.CreateList<Wallet>();
            walletCoinDtoInput = FixureManger.Create<WalletCoinDtoInput>();
            walletCoinDto = FixureManger.Create<WalletCoinDto>();
            walletCoin = FixureManger.Create<WalletCoin>();

            walletId = walletDto.Id;
            notExistingWallet = Guid.NewGuid();

            provider = new Mock<IRepository<Wallet>>();
            mapper = new Mock<IDataMapper>();

            provider.Setup(provider => provider.CreateAsync(It.IsAny<Wallet>())).ReturnsAsync(walletEntity);
            provider.Setup(provider => provider.GetAsync(walletId)).ReturnsAsync(walletEntity);
            provider.Setup(provider => provider.GetAsync(notExistingWallet)).ReturnsAsync((Wallet)null!);
            provider.Setup(provider => provider.DeleteAsync(walletId)).ReturnsAsync(true);
            provider.Setup(provider => provider.GetAllAsync()).ReturnsAsync(walletEntityList);
            provider.Setup(provider => provider.UpdateAsync(walletEntity)).ReturnsAsync(walletEntity);

            mapper.Setup(mapper => mapper.Map<Wallet>(recivedWallet)).Returns(walletEntity);
            mapper.Setup(mapper => mapper.Map<WalletDto>(walletEntity)).Returns(walletDto);
            mapper.Setup(mapper => mapper.Map<IEnumerable<WalletDto>>(walletEntityList)).Returns(walletDtoList);
            mapper.Setup(mapper => mapper.Map<WalletCoin>(walletCoinDtoInput)).Returns(walletCoin);


            walletService = new WalletService(provider.Object, mapper.Object);
        }
        [Test]
        public async Task CreateAsync_Retrurn_WalletDto()
        {
            var actualWallet = await walletService.CreateAsync(recivedWallet);

            Assert.That(walletDto, Is.EqualTo(actualWallet));
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
            var actualWallet = await walletService.GetAsync(walletId);

            provider.Verify(provider => provider.GetAsync(walletId), Times.Once);
        }

        [Test]
        public async Task GetWalletAsync_Return_WalletDto()
        {
            var actualWallet = await walletService.GetAsync(walletId);

            Assert.That(walletDto, Is.EqualTo(actualWallet));
        }

        [Test]
        public async Task DeleteWalletAsync_DeleteAsync_CalledOnce()
        {
            var ret = await walletService.DeleteAsync(walletId);

            provider.Verify(provider => provider.DeleteAsync(walletId), Times.Once);
        }

        [Test]
        public async Task DeleteWalletAsync_ReturnTrue()
        {
            var ret = await walletService.DeleteAsync(walletId);

            Assert.That(ret, Is.True);
        }

        [Test]
        public async Task DeleteWalletAsync_DeleteAsyncFalse_ReturnFalse()
        {
            provider.Setup(provider => provider.DeleteAsync(walletId)).ReturnsAsync(false);

            var ret = await walletService.DeleteAsync(walletId);

            Assert.That(ret, Is.False);
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

            Assert.That(walletDtoList, Is.EqualTo(wallets));
        }

        [Test]
        public async Task AddCoin_CoinNotExist_ReturnDefault()
        {
            var wallet = await walletService.AddCoin(notExistingWallet, walletCoinDtoInput);

            Assert.That(wallet, Is.EqualTo(default(WalletDto)));
        }

        [Test]
        public async Task AddCoin_GetCoin_CalledOnce()
        {
            var wallet = await walletService.AddCoin(walletId, walletCoinDtoInput);

            provider.Verify(provider => provider.GetAsync(walletId), Times.Once);
        }

        [Test]
        public async Task AddCoin_GetCoin_CoinAdded()
        {
            var walletExpected = await walletService.AddCoin(walletId, walletCoinDtoInput);

            _ = walletDto.Coins.Append(walletCoinDto);

            Assert.That(walletExpected, Is.EqualTo(walletDto));
        }

        [Test]
        public async Task AddCoin_UpdateCoin_CalledOnce()
        {
            var wallet = await walletService.AddCoin(walletId, walletCoinDtoInput);

            provider.Verify(provider => provider.UpdateAsync(walletEntity), Times.Once);
        }

        [Test]
        public async Task AddCoin_MapWalletCoinDto_CalledOnce()
        {
            var wallet = await walletService.AddCoin(walletId, walletCoinDtoInput);

            mapper.Verify(mapper => mapper.Map<WalletCoin>(walletCoinDtoInput), Times.Once);
        }
    }
}
