using AutoMapper;
using CoinTracker.API.CoinList.Application.Common.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.Api.CoinList.Infrastructure.Mapper
{
    public class AutoMapperDataMapper : IDataMapper
    {
        private readonly IMapper mapper;

        public AutoMapperDataMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TDestination Map<TDestination>(object sourceObj)
        {
            return mapper.Map<TDestination>(sourceObj);
        }
    }
}
