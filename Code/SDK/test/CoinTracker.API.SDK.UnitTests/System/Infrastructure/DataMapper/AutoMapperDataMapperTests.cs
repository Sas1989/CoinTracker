using API.SDK.Infrastructure.DataMapper;
using AutoMapper;

namespace API.SDK.UnitTests.System.Infrastructure.DataMapper
{
    internal class AutoMapperDataMapperTests
    {
        [Test]
        public void Map_CallAutoMapper_CalledOnce()
        {
            var mapperMoq = new Mock<IMapper>();
            var dataMapper = new AutoMapperDataMapper(mapperMoq.Object);

            dataMapper.Map<It.IsAnyType>(It.IsAny<object>());

            mapperMoq.Verify(mapper => mapper.Map<It.IsAnyType>(It.IsAny<object>()), Times.Once);
        }
    }
}
