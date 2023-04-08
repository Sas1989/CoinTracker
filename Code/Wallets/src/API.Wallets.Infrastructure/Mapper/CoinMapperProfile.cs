using API.Wallets.Domain.Dtos.Coin;
using API.Wallets.Domain.Entities;
using AutoMapper;
using CoinTracker.API.Contracts.Coin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Infrastructure.Mapper
{
    public class CoinMapperProfile : Profile
    {
        public  CoinMapperProfile()
        {
            CreateMap<CoinInsert, CoinDto>();
            CreateMap<CoinUpdate, CoinDto>();
            CreateMap<CoinDto, Coin>();
        }
    }
}
