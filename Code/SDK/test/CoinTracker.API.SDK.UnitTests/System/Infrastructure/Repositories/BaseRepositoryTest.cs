using API.SDK.Domain.Persistence.DataProvider;
using API.SDK.Infrastructure.Repositories;
using API.SDK.UnitTests.Fixure.Models;
using API.SDK.UnitTests.Fixure.Repositories;

namespace API.SDK.UnitTests.System.Infrastructure.Repositories;

internal class BaseRepositoryTest
{
    private IDataProvider<FakeEntity> dataProvider;
    private FakeBaseRepository bRepository;
    private FakeEntity entity;
    private Guid entityId;

    [SetUp]
    public void Setup()
    {
        dataProvider = Substitute.For<IDataProvider<FakeEntity>>();
        bRepository = new FakeBaseRepository(dataProvider);

        entity = FixureManger.Create<FakeEntity>();
        entityId = entity.Id;
    }

    [Test]
    public async Task SaveAsync_Save_CalledOnceAsync()
    {
        await bRepository.Save(entity);

        await dataProvider.Received(1).SaveAsync(entity);
    }

    [Test]
    public async Task SaveAsync_ShouldNeverCallSave_WhenValidationIsFalse()
    {
        bRepository.ValidationReturn = false;

        await bRepository.Save(entity);

        await dataProvider.Received(0).SaveAsync(entity);
    }

    [Test]
    public async Task DeleteAsync_Delete_CalledOnceAsync()
    {
        await bRepository.DeleteAsync(entity);

        await dataProvider.Received(1).DeleteAsync(entity.Id);
    }


    [Test]
    public async Task GetAll_GetAll_CalledOnce()
    {
        await bRepository.GetAllAsync();

        await dataProvider.Received(1).GetAllAsync();
    }

    [Test]
    public async Task Get_GetAll_CalledOnce()
    {
        await bRepository.GetByIdAsync(entityId);

        await dataProvider.Received(1).GetByIdAsync(entityId);
    }

}
