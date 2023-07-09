using API.Wallets.Domain.Dtos.Wallet;
using API.Wallets.Domain.Entities;
using API.Wallets.Domain.Entities.Wallet;
using AutoMapper;

namespace API.Wallets.Infrastructure.Mapper
{
    public class WalletMapperProfile : Profile
    {
        public WalletMapperProfile()
        {
            CreateMap<WalletDtoInput, Wallet>();
            CreateMap<Wallet, WalletDto>();
        }
    }
}
