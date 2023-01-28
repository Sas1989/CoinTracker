using AutoMapper;
using CoinTracker.API.SDK.Infrastructure.DataMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.SDK.UnitTests.System.Infrastructure.DataMapper
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
