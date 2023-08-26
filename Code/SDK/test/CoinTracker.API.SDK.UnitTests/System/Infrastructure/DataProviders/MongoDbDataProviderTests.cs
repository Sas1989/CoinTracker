using API.SDK.Infrastructure.DataProviders;
using API.SDK.UnitTests.Fixure.Models;
using MongoDB.Driver;
using NSubstitute.ReceivedExtensions;

namespace API.SDK.UnitTests.System.Infrastructure.DataProviders;

internal class MongoDbDataProviderTests
{
    private IMongoCollection<FakeEntity> mongoCollection;
    private IMongoDatabase mongoDatabase;
    private MongoDbDataProvider<FakeEntity> provider;
    private FakeEntity entity;
    private IEnumerable<FakeEntity> entities;
    private long countReturn;
    private Guid id;

    [SetUp]
    public void Setup()
    {
        mongoCollection = Substitute.For<IMongoCollection<FakeEntity>>();
        mongoDatabase = Substitute.For<IMongoDatabase>();

        mongoDatabase.GetCollection<FakeEntity>(Arg.Any<string>()).Returns(mongoCollection);

        provider = new MongoDbDataProvider<FakeEntity>(mongoDatabase);

        entity = FixureManger.Create<FakeEntity>();
        entities = FixureManger.CreateList<FakeEntity>();
        countReturn = FixureManger.Create<long>();

        id = Guid.NewGuid();

        mongoCollection.CountDocumentsAsync(Arg.Any<FilterDefinition<FakeEntity>>()).Returns(countReturn);

    }

    [Test]
    public void SaveAsync_EmptyEntity_ThrowException()
    {
        Task func() => provider.SaveAsync((FakeEntity)null!);

        var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
        Assert.That(ex.ParamName, Is.EqualTo("entity"));
        mongoCollection.DidNotReceive().ReplaceOneAsync(Arg.Any<FilterDefinition<FakeEntity>>(), Arg.Any<FakeEntity>());
    }

    [Test]
    public async Task CreateAsync_ReplaceOneAsync_CalledOne()
    {
        await provider.SaveAsync(entity);

        await mongoCollection.Received(1).ReplaceOneAsync(Arg.Any<FilterDefinition<FakeEntity>>(),entity);
    }

    [Test]
    public async Task DeleteByIdAsync_DeleteOneAsync_CalledOnce()
    {

        await provider.DeleteAsync(id);

        await mongoCollection.Received(1).DeleteOneAsync(Arg.Any<FilterDefinition<FakeEntity>>());
    }


    [Test]
    public void BulkSaveListAsync_EntityIsNull_ThrowException()
    {
        Task func() => provider.SaveAsync((List<FakeEntity>)null!);

        var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
        Assert.That(ex.ParamName, Is.EqualTo("entityList"));
    }

    [Test]
    public async Task BulkSaveListAsync_ReplaceOneAsync_CalledOnceAsync()
    {
        await provider.SaveAsync(entities);

        await mongoCollection.Received(1).BulkWriteAsync(Arg.Any<List<WriteModel<FakeEntity>>>());
    }

    [Test]
    public async Task Count_CountDocuments_CalledOnce()
    {
        await provider.Count();

        await mongoCollection.Received(1).CountDocumentsAsync(Arg.Any<FilterDefinition<FakeEntity>>());
    }

    [Test]
    public async Task Count_ReturnAGoodVaule()
    {
        var result = await provider.Count();

        Assert.That(result, Is.EqualTo(countReturn));
    }

    [Test]
    public async Task CountFilter_CountDocuments_CalledOnce()
    {
        await provider.Count(entity => entity.Id == id);
        await mongoCollection.Received(1).CountDocumentsAsync(Arg.Any<FilterDefinition<FakeEntity>>());
    }

    [Test]
    public async Task CountFilter_ReturnAGoodVaule()
    {
        var result = await provider.Count(entity => entity.Id == id);

        Assert.That(result, Is.EqualTo(countReturn));
    }
}
