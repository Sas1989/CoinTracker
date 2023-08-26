using API.SDK.Domain.Persistence.DataProvider;
using API.SDK.Infrastructure.Repositories;
using API.SDK.UnitTests.Fixure.Models;

namespace API.SDK.UnitTests.Fixure.Repositories;

internal class FakeBaseRepository : BaseRepository<FakeEntity>
{
    public bool ValidationReturn = true;
    internal FakeBaseRepository(IDataProvider<FakeEntity> dataProvider) : base(dataProvider)
    {
    }

    protected override bool Validation(FakeEntity entity)
    {
        return ValidationReturn;
    }
}
