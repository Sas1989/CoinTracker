using API.CoinList.Domain.Dtos;
using API.CoinList.Domain.Entities;
using API.Contracts.Coin;
using AutoMapper;

namespace API.CoinList.Infrastructure.Mapper
{
    public class CoinMapperProfile : Profile
    {
        public CoinMapperProfile()
        {
            CreateMap<Coin, CoinDto>();
            CreateMap<CoinDtoInput, Coin>();
            CreateMap<CoinDto, CoinInsert>();
            CreateMap<CoinDto, CoinUpdate>();
        }
    }
}
