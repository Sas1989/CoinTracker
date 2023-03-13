using AutoMapper;
using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.Domain.Entities;
using CoinTracker.API.Contracts.Coin;

namespace CoinTracker.Api.CoinList.Infrastructure.Mapper
{
    public class CoinMapperProfile : Profile
    {
        public CoinMapperProfile()
        {
            CreateMap<Coin, CoinDto>();
            CreateMap<RecivedCoinDto, Coin>();
            CreateMap<CoinDto, CoinInsert>();
            CreateMap<CoinDto, CoinUpdate>();
        }
    }
}
