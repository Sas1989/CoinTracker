using API.SDK.Domain.Persistence.DataProvider;
using API.SDK.Infrastructure.Cache;
using API.SDK.UnitTests.Fixure.Models;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute.ExceptionExtensions;
using System.Linq.Expressions;

namespace API.SDK.UnitTests.System.Infrastructure.Cache;

internal class CachedDataProviderTest
{
    private IMemoryCache cache;
    private IDataProvider<FakeEntity> provider;
    private CachedDataProvider<FakeEntity> cacheManager;
    private IEnumerable<FakeEntity> entityList;
    private FakeEntity entity;

    [SetUp]
    public void Setup()
    {
        cache = Substitute.For<IMemoryCache>();
        provider = Substitute.For<IDataProvider<FakeEntity>>();
        cacheManager = new CachedDataProvider<FakeEntity>(provider,cache);

        entityList = FixureManger.CreateList<FakeEntity>();
        entity = FixureManger.Create<FakeEntity>();
    }

    [Test]
    public async Task Count_ShouldCallCount()
    {
        await cacheManager.Count();

        await provider.Received(1).Count();
    }

    [Test]
    public async Task CountExpression_ShouldCallCount()
    {
        await cacheManager.Count((entity) => entity.Id == entity.Id);

        await provider.Received(1).Count(Arg.Any<Expression<Func<FakeEntity, bool>>>());
    }

    [Test]
    public async Task SaveAsync_ShouldCallSaveAsync_Once()
    {
        await cacheManager.SaveAsync(entity);

        await provider.Received(1).SaveAsync(entity);
    }

    [Test]
    public async Task SaveAsync_ShouldAddEntityInTheCacheAsync()
    {
        await cacheManager.SaveAsync(entity);

        cache.Received(1).Set(entity.Id, entity);
    }

    [Test]
    public void SaveAsync_ShouldNotFillTheCash_WhenSaveAsyncTriggerAnError()
    {
        provider.SaveAsync(entity).Throws<Exception>();

        Assert.ThrowsAsync<Exception>(async () => await cacheManager.SaveAsync(entity));

        cache.DidNotReceive().Set(entity.Id, entity);
    }

    [Test]
    public async Task DeleteAsync_ShouldCallDeleteAsyncOnceAsync()
    {
        await cacheManager.DeleteAsync(entity.Id);

        await provider.Received(1).DeleteAsync(entity.Id);
    }       

    [Test]
    public async Task DeleteAsync_ShouldDeleteEntityInTheCacheAsync()
    {
        await cacheManager.DeleteAsync(entity.Id);

        cache.Received(1).Remove(entity.Id);
    }


    [Test]
    public async Task FilterAsync_ShouldCallFilterAsyncOnceAsync()
    {
        await cacheManager.FilterAsync((entity) => entity.Id == entity.Id);

        await provider.Received(1).FilterAsync(Arg.Any<Expression<Func<FakeEntity, bool>>>());
    }

    [Test]
    public async Task GetAllAsync_ShouldCallGetAllAsyncOnceAsync()
    {
        await cacheManager.GetAllAsync();

        await provider.Received(1).GetAllAsync();
    }

    [Test]
    public async Task GetByIDAsync_ShouldNotCallDatabase_WhenEntityIsOnCache()
    {
        cache.TryGetValue(entity.Id, out FakeEntity? _).Returns(true);

        await cacheManager.GetByIdAsync(entity.Id);

        await provider.DidNotReceive().GetByIdAsync(Arg.Any<Guid>());
    }

    [Test]
    public async Task GetByIDAsync_ShouldCallDatabase_WhenEntityIsNotOnCache()
    {
        cache.TryGetValue(entity.Id, out FakeEntity? _).Returns(false);

        await cacheManager.GetByIdAsync(entity.Id);

        await provider.Received(1).GetByIdAsync(Arg.Any<Guid>());
    }
    [Test]
    public async Task SaveBulkAsync_ShouldCallRepalceAsync_Once()
    {
        await cacheManager.SaveAsync(entity);

        await provider.Received(1).SaveAsync(entity);
    }

    [Test]
    public async Task SaveBulkAsync_ShouldAddAllEntityInTheCacheAsync()
    {
        await cacheManager.SaveAsync(entityList);

        foreach (var entity in entityList)
        {
            cache.Received(1).Set(entity.Id, entity);
        }
    }

}