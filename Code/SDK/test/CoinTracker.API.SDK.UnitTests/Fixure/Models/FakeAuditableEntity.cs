using API.SDK.Domain.Entities;

namespace API.SDK.UnitTests.Fixure.Models;

internal class FakeAuditableEntity : AuditableEntity
{
    private FakeAuditableEntity() : base() { }

    public static FakeAuditableEntity Create()
    {
        return new FakeAuditableEntity();
    }

    public void Update()
    {
        ChangeUpdateTime();
    }

}
