﻿using API.SDK.Application.DataMapper;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SDK.Infrastructure.DataMapper
{
    public class AutoMapperDataMapper : IDataMapper
    {
        private readonly IMapper mapper;

        public AutoMapperDataMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TDestination Map<TDestination>(object? sourceObj)
        {
            return mapper.Map<TDestination>(sourceObj);
        }
    }
}
