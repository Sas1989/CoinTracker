﻿using API.SDK.Infrastructure.Repository;
using API.SDK.UnitTests.Fixure.Models;
using MongoDB.Driver;

namespace API.SDK.UnitTests.System.Infrastructure.Providers
{
    internal class MongoDbProviderTests
    {
        private Mock<IMongoCollection<FakeEntity>> mongoCollection;
        private MongoDbRepository<FakeEntity> provider;
        private FakeEntity entity;
        private IEnumerable<FakeEntity> entities;

        [SetUp]
        public void Setup()
        {
            mongoCollection = new Mock<IMongoCollection<FakeEntity>>();
            provider = new MongoDbRepository<FakeEntity>(mongoCollection.Object);

            entity = FixureManger.Create<FakeEntity>();
            entities = FixureManger.CreateList<FakeEntity>();
        }

        [Test]
        public void CreateAsync_EmptyEntity_ThrowException()
        {

            var func = () => provider.CreateAsync((FakeEntity)null!);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("entity"));
        }

        [Test]
        public async Task CreateAsync_InsertOneAsync_CalledOne()
        {

            await provider.CreateAsync(entity);

            mongoCollection.Verify(collection => collection.InsertOneAsync(entity, null, default), Times.Once);
        }
        [Test]
        public async Task CreateAsync_Coin_Returned()
        {
            var result = await provider.CreateAsync(entity);

            Assert.That(result, Is.EqualTo(entity));
        }
        [Test]
        public async Task RemoveAsync_DeleteOneAsync_CalledOnce()
        {
            Guid id = Guid.NewGuid();

            var deleteOneResult = new Mock<DeleteResult>();
            deleteOneResult.SetupGet(deleteOneResult => deleteOneResult.IsAcknowledged).Returns(true);

            mongoCollection.Setup(collection => collection.DeleteOneAsync(It.IsAny<FilterDefinition<FakeEntity>>(), default)).ReturnsAsync(deleteOneResult.Object);

            await provider.DeleteAsync(id);
            mongoCollection.Verify(collection => collection.DeleteOneAsync(It.IsAny<FilterDefinition<FakeEntity>>(), default), Times.Once);
        }

        [Test]
        public async Task RemoveAsync_DeleteOneAsync_ReturnFalse_ReturnFalse()
        {
            Guid id = Guid.NewGuid();

            var deleteOneResult = new Mock<DeleteResult>();
            deleteOneResult.SetupGet(deleteOneResult => deleteOneResult.DeletedCount).Returns(0);

            mongoCollection.Setup(collection => collection.DeleteOneAsync(It.IsAny<FilterDefinition<FakeEntity>>(), default)).ReturnsAsync(deleteOneResult.Object);

            var result = await provider.DeleteAsync(id);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveAsync_DeleteOneAsync_ReturnTrue_ReturnTrue()
        {
            Guid id = Guid.NewGuid();

            var deleteOneResult = new Mock<DeleteResult>();
            deleteOneResult.SetupGet(deleteOneResult => deleteOneResult.DeletedCount).Returns(1);

            mongoCollection.Setup(collection => collection.DeleteOneAsync(It.IsAny<FilterDefinition<FakeEntity>>(), default)).ReturnsAsync(deleteOneResult.Object);

            var result = await provider.DeleteAsync(id);

            Assert.That(result, Is.True);
        }

        [Test]
        public void UpdateAsync_EntityIsNull_ThrowException()
        {
            var func = () => provider.UpdateAsync(null!);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("entity"));
        }
        [Test]
        public void UpdateAsync_ReplaceOneAsync_CalledOnce()
        {

            var replaceOneResult = new Mock<ReplaceOneResult>();
            replaceOneResult.SetupGet(replaceOneResult => replaceOneResult.IsAcknowledged).Returns(true);
            replaceOneResult.SetupGet(replaceOneResult => replaceOneResult.MatchedCount).Returns(1);

            mongoCollection.Setup(collection => collection.ReplaceOneAsync(It.IsAny<FilterDefinition<FakeEntity>>(), entity, It.IsAny<ReplaceOptions>(), default)).ReturnsAsync(replaceOneResult.Object);

            _ = provider.UpdateAsync(entity);

            mongoCollection.Verify(collection => collection.ReplaceOneAsync(It.IsAny<FilterDefinition<FakeEntity>>(), entity, It.IsAny<ReplaceOptions>(), default), Times.Once);
        }

        [Test]
        public void CreateAsyncMultiple_EntityListIsNull_ThrowException()
        {

            var func = () => provider.CreateAsync((IEnumerable<FakeEntity>)null!);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("entities"));
        }

        [Test]
        public async Task CreateAsyncMultiple_InserMany_CalledOnce()
        {
            await provider.CreateAsync(entities);

            mongoCollection.Verify(x => x.InsertManyAsync(entities, null, default), Times.Once);
        }
        [Test]
        public async Task CreateAsyncMultiple_Return()
        {
            var result = await provider.CreateAsync(entities);

            Assert.That(result, Is.EqualTo(entities));

        }
    }
}
