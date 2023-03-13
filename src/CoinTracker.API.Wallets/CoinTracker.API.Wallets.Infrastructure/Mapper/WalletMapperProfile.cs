using AutoMapper;
using CoinTracker.API.Wallets.Domain.Dtos;
using CoinTracker.API.Wallets.Domain.Entities;

namespace CoinTracker.API.Wallets.Infrastructure.Mapper
{
    public class WalletMapperProfile : Profile
    {
        public WalletMapperProfile()
        {
            CreateMap<RecivedWalletDto, Wallet>();
            CreateMap<Wallet, WalletDto>();
        }
    }
}
