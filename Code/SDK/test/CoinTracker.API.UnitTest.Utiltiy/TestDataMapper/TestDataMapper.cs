using API.SDK.Application.DataMapper;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.UnitTest.Utility.TestDataMapper
{
    public  abstract class TestDataMapper : IDataMapper
    {
        private IMapper _mapper;
        private MapperConfigurationExpression _mappingConfiguration = new MapperConfigurationExpression();
        public TestDataMapper()
        {
            LoadConfiguration();
            var config = new MapperConfiguration(_mappingConfiguration);

            _mapper = config.CreateMapper();
        }

        public TDestination Map<TDestination>(object? sourceObj)
        {
            return _mapper.Map<TDestination>(sourceObj);
        }

        protected void AddMapping<TDestination, TSource>()
        {
            _mappingConfiguration.CreateMap<TSource, TDestination>();
        }

        protected abstract void LoadConfiguration();
    }
}
