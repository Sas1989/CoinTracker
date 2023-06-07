using API.SDK.Application.ApplicationService;
using API.SDK.Application.DataMapper;
using API.SDK.Application.Provider;
using API.Wallets.Application.Services.Interfaces;
using API.Wallets.Domain.Dtos.Coin;
using API.Wallets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Application.Services
{
    public class CoinService : ApplicationService<Coin, CoinDto, CoinDto>, ICoinService
    {
        public CoinService(IProvider<Coin> provider, IDataMapper mapper) : base(provider, mapper)
        {
        }
    }
}
