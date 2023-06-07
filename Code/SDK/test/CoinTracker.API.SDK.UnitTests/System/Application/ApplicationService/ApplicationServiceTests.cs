using API.SDK.Application.ApplicationService;
using API.SDK.Application.DataMapper;
using API.SDK.Application.Provider;
using API.SDK.Domain.Exceptions;
using API.SDK.UnitTests.Fixure;
using API.SDK.UnitTests.Fixure.Models;

namespace API.SDK.UnitTests.System.Application.ApplicationService
{
    public class ApplicationServiceTests
    {
        private Mock<IProvider<FakeEntity>> provider;
        private Mock<IDataMapper> mapper;
        private FakeRecivedDto recivedDto;
        private FakeDto dto;
        private FakeEntity entity;
        private IEnumerable<FakeRecivedDto?> recivedDtoList;
        private IEnumerable<FakeEntity> entityList;
        private IEnumerable<FakeDto> dtoList;
        private ApplicationService<FakeEntity, FakeDto, FakeRecivedDto?> applicationService;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<IProvider<FakeEntity>>();
            mapper = new Mock<IDataMapper>();

            recivedDto = GenerateFakeModels.GenerateRecivedDto();
            dto = GenerateFakeModels.GenerateDto();
            entity = GenerateFakeModels.GenerateEntity();

            recivedDtoList = GenerateFakeModels.GenerateRecivedDtoList();
            entityList = GenerateFakeModels.GenerateEntityList();
            dtoList = GenerateFakeModels.GenerateDtoList();

            applicationService = new ApplicationService<FakeEntity, FakeDto, FakeRecivedDto?>(provider.Object, mapper.Object);

            provider.Setup(provider => provider.CreateAsync(entity)).ReturnsAsync(entity);
            provider.Setup(provider => provider.CreateAsync(entityList)).ReturnsAsync(entityList);
            provider.Setup(provider => provider.DeleteAsync(entity.Id)).ReturnsAsync(true);
            provider.Setup(provider => provider.GetAllAsync()).ReturnsAsync(entityList);
            provider.Setup(provider => provider.GetAsync(dto.guidProp)).ReturnsAsync(entity);
            provider.Setup(provider => provider.UpdateAsync(entity)).ReturnsAsync(entity);

            mapper.Setup(mapper => mapper.Map<FakeDto>(entity)).Returns(dto);
            mapper.Setup(mapper => mapper.Map<FakeEntity>(recivedDto)).Returns(entity);
            mapper.Setup(mapper => mapper.Map<IEnumerable<FakeDto>>(entityList)).Returns(dtoList);
            mapper.Setup(mapper => mapper.Map<IEnumerable<FakeEntity>>(recivedDtoList)).Returns(entityList);

        }

        [Test]
        public async Task CreateAsync_CreateAsync_CalledOnceAsync()
        {
            await applicationService.CreateAsync(recivedDto);

            provider.Verify(provider => provider.CreateAsync(It.IsAny<FakeEntity>()), Times.Once());
        }

        [Test]
        public async Task CreateAsync_Mapped_CalledOnceAsync()
        {

            await applicationService.CreateAsync(recivedDto);

            mapper.Verify(mapper => mapper.Map<FakeEntity>(recivedDto), Times.Once());
        }

        [Test]
        public async Task CreateAsync_ReturnDto()
        {

            var result = await applicationService.CreateAsync(recivedDto);

            Assert.That(result, Is.EqualTo(dto));
        }

        [Test]
        public void CreateAsync_RecivedDtoIsNull_ThowException()
        {
            var func = () => applicationService.CreateAsync(null);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("recivedDto"));
        }

        [Test]
        public async Task CreateMultipleAsync_CreateAsync_CalledOnce()
        {
            var result = await applicationService.CreateMultipleAsync(recivedDtoList);

            provider.Verify(x => x.CreateAsync(entityList), Times.Once);
        }

        [Test]
        public async Task CreateMultipleAsync_ReturnsDtoList()
        {
            var result = await applicationService.CreateMultipleAsync(recivedDtoList);

            Assert.That(result, Is.EqualTo(dtoList));

        }

        [Test]
        public async Task DeleteAsync_DeleteAsync_CalledOnce()
        {
            var result = await applicationService.DeleteAsync(entity.Id);

            provider.Verify(provider => provider.DeleteAsync(entity.Id), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_DeleteAsync_ReturnFalse_ReturnFalse()
        {
            provider.Setup(provider => provider.DeleteAsync(entity.Id)).ReturnsAsync(false);

            var result = await applicationService.DeleteAsync(entity.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteCoin_DeleteAsync_ReturnTrue_ReturnTrue()
        {
            var result = await applicationService.DeleteAsync(entity.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAllAsync_GetAllAsync_CalledOnce()
        {
            await applicationService.GetAllAsync();

            provider.Verify(provider => provider.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_Map_CalledOne()
        {
            await applicationService.GetAllAsync();

            mapper.Verify(mapper => mapper.Map<IEnumerable<FakeDto>>(entityList), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_CoinDto_ReturnedCorrect()
        {
            var result = await applicationService.GetAllAsync();

            Assert.That(result, Is.EqualTo(dtoList));

        }

        [Test]
        public async Task GetAync_GetAsync_CalledOnce()
        {

            var result = await applicationService.GetAsync(dto.guidProp);

            provider.Verify(provider => provider.GetAsync(dto.guidProp), Times.Once);
        }

        [Test]
        public async Task GetAync_EntityNotFound_TrowException()
        {

            provider.Setup(provider => provider.GetAsync(dto.guidProp)).ReturnsAsync((FakeEntity)null);

            var func = () => applicationService.GetAsync(dto.guidProp);

            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => func());
        }

        [Test]
        public async Task GetAync_CoinDto_ReturnedCorrect()
        {
            var result = await applicationService.GetAsync(dto.guidProp);

            Assert.That(result, Is.EqualTo(dto));
        }

        [Test]
        public async Task UpdateAsync_Mapper_CalledOnce()
        {
            await applicationService.UpdateAsync(dto.guidProp, recivedDto);

            mapper.Verify(mapper => mapper.Map<FakeEntity>(recivedDto), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_Update_CalledOnce()
        {
            await applicationService.UpdateAsync(dto.guidProp, recivedDto);

            provider.Verify(provider => provider.UpdateAsync(entity), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_Retruns_Dto()
        {
            var result = await applicationService.UpdateAsync(dto.guidProp, recivedDto);

            Assert.That(result, Is.EqualTo(dto));
        }

        [Test]
        public async Task UpdateAsync_EntityNotFound_TrowException()
        {
            provider.Setup(provider => provider.UpdateAsync(entity)).ReturnsAsync((FakeEntity)null);

            var func = () => applicationService.UpdateAsync(dto.guidProp, recivedDto);

            var ex = Assert.ThrowsAsync<EntityNotFoundException>(() => func());
        }

        [Test]
        public async Task UpdateAsync_EntityRecivedDto_TrowException()
        {
            var func = () => applicationService.UpdateAsync(dto.guidProp, null);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("recivedDto"));
        }

    }
}