using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.Domain.Entities;
using CoinTracker.API.Contracts.Coins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.Api.CoinList.Infrastructure.Mapper
{
    public class CoinMapper : AutoMapper.Profile
    {
        public CoinMapper()
        {
            CreateMap<Coin, CoinDto>();
            CreateMap<RecivedCoinDto, Coin>();
            CreateMap<CoinDto, CoinInsert>();
            CreateMap<CoinDto, CoinUpdate>();
        }
    }
}
