using CoinTracker.Api.CoinList.Infrastructure.Providers;
using CoinTracker.API.CoinList.Domain.Entities;
using CoinTracker.API.CoinList.UnitTests.Fixture;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.UnitTests.System.Infrastructure
{
    internal class MongoDbProviderTests
    {
        private Mock<IMongoCollection<Coin>> mongoCollection;
        private MongoDbProvider provider;
        private Coin coin;

        [SetUp]
        public void Setup()
        {
            mongoCollection = new Mock<IMongoCollection<Coin>>();
            provider = new MongoDbProvider(mongoCollection.Object);

            coin = CoinFixture.GenerateCoin();
        }

        [Test]
        public async Task CreateAsync_EmptyEntity_ThrowException()
        {

            var func = () => provider.CreateAsync((Coin)null);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("entity"));
        }

        [Test]
        public async Task CreateAsync_InsertOneAsync_CalledOne()
        {

            await provider.CreateAsync(coin);

            mongoCollection.Verify(collection => collection.InsertOneAsync(coin, null, default), Times.Once);
        }
        [Test]
        public async Task CreateAsync_Coin_Returned()
        {
            var result = await provider.CreateAsync(coin);

            Assert.AreEqual(coin, result);
        }
        [Test]
        public async Task RemoveAsync_DeleteOneAsync_CalledOnce()
        {
            Guid id = Guid.NewGuid();

            var deleteOneResult = new Mock<DeleteResult>();
            deleteOneResult.SetupGet(deleteOneResult => deleteOneResult.IsAcknowledged).Returns(true);

            mongoCollection.Setup(collection => collection.DeleteOneAsync(It.IsAny<FilterDefinition<Coin>>(), default)).ReturnsAsync(deleteOneResult.Object);

            await provider.DeleteAsync(id);
            mongoCollection.Verify(collection => collection.DeleteOneAsync(It.IsAny<FilterDefinition<Coin>>(), default), Times.Once);
        }

        [Test]
        public async Task RemoveAsync_DeleteOneAsync_ReturnFalse_ReturnFalse()
        {
            Guid id = Guid.NewGuid();

            var deleteOneResult = new Mock<DeleteResult>();
            deleteOneResult.SetupGet(deleteOneResult => deleteOneResult.DeletedCount).Returns(0);

            mongoCollection.Setup(collection => collection.DeleteOneAsync(It.IsAny<FilterDefinition<Coin>>(), default)).ReturnsAsync(deleteOneResult.Object);

            var result = await provider.DeleteAsync(id);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveAsync_DeleteOneAsync_ReturnTrue_ReturnTrue()
        {
            Guid id = Guid.NewGuid();

            var deleteOneResult = new Mock<DeleteResult>();
            deleteOneResult.SetupGet(deleteOneResult => deleteOneResult.DeletedCount).Returns(1);

            mongoCollection.Setup(collection => collection.DeleteOneAsync(It.IsAny<FilterDefinition<Coin>>(), default)).ReturnsAsync(deleteOneResult.Object);

            var result = await provider.DeleteAsync(id);

            Assert.That(result, Is.True);
        }

        [Test]
        public void UpdateAsync_EntityIsNull_ThrowException()
        {
            var func = () => provider.UpdateAsync(null);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("entity"));
        }
        [Test]
        public async Task UpdateAsync_ReplaceOneAsync_CalledOnce()
        {
            
            var replaceOneResult = new Mock<ReplaceOneResult>();
            replaceOneResult.SetupGet(replaceOneResult => replaceOneResult.IsAcknowledged).Returns(true);
            replaceOneResult.SetupGet(replaceOneResult => replaceOneResult.MatchedCount).Returns(1);

            mongoCollection.Setup(collection => collection.ReplaceOneAsync(It.IsAny<FilterDefinition<Coin>>(), coin, It.IsAny<ReplaceOptions>(), default)).ReturnsAsync(replaceOneResult.Object);

            provider.UpdateAsync(coin);

            mongoCollection.Verify(collection => collection.ReplaceOneAsync(It.IsAny<FilterDefinition<Coin>>(), coin, It.IsAny<ReplaceOptions>(), default), Times.Once);
        }

        [Test]
        public async Task CreateAsyncMultiple_EntityListIsNull_ThrowException() {

            var func = () => provider.CreateAsync((IEnumerable<Coin>)null);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => func());
            Assert.That(ex.ParamName, Is.EqualTo("entities"));
        }

        [Test]
        public async Task CreateAsyncMultiple_InserMany_CalledOnce()
        {
            IEnumerable<Coin> coins = CoinFixture.GenereteListOfCoin();
            await provider.CreateAsync(coins);

            mongoCollection.Verify(x => x.InsertManyAsync(coins,null,default), Times.Once);
        }
        [Test]
        public async Task CreateAsyncMultiple_Return()
        {
            IEnumerable<Coin> coins = CoinFixture.GenereteListOfCoin();
            var result = await provider.CreateAsync(coins);

            Assert.AreEqual(coins, result);
            
        }
    }
}
