using AutoMapper;
using CoinTracker.Api.CoinList.Infrastructure.Mapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.UnitTests.System.Infrastructure
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
