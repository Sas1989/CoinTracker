using API.SDK.Application.ApplicationService;
using API.Wallets.Domain.Dtos.Coin;
using API.Wallets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Application.Services
{
    public interface ICoinService : IApplicationService<Coin, CoinDto, CoinDto>
    {
    }
}
