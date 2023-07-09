using API.SDK.Application.DataMapper;
using API.SDK.Application.Repository;
using API.SDK.Infrastructure.Services;
using API.Wallets.Application.Services;
using API.Wallets.Domain.Dtos.Coin;
using API.Wallets.Domain.Entities;

namespace API.Wallets.Infrastructure.Services
{
    public class CoinService : ApplicationService<Coin, CoinDto, CoinDto>, ICoinService
    {
        public CoinService(IRepository<Coin> provider, IDataMapper mapper) : base(provider, mapper)
        {
        }
    }
}
