using API.SDK.UnitTests.Fixure.Models;

namespace API.SDK.UnitTests.System.Entities;

internal class AuditableEntityTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Create_ShouldSetTheCreationTime()
    {
        var now = DateTime.Now;
        var entity = FakeAuditableEntity.Create();

        Assert.That(entity.CreationDate, Is.GreaterThan(now));
    }

    [Test]
    public void Update_ShouldSetTheUpdateTime()
    {
        var entity = FakeAuditableEntity.Create();

        entity.Update();

        Assert.That(entity.UpdateDate, Is.GreaterThan(entity.CreationDate));
    }
}
