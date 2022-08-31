using CoinTracker.API.CoinList.Application.Common.Mappers;
using CoinTracker.API.CoinList.Application.Providers;
using CoinTracker.API.CoinList.Application.Services.Interfaces;
using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Application.Services
{
    public class CoinService : ICoinService
    {
        private readonly IProvider provider;
        private readonly IDataMapper mapper;

        public CoinService(IProvider provider, IDataMapper mapper)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        public async Task<CoinDto> CreateAsync(RecivedCoinDto recivedCoin)
        {
            Coin coin = mapper.Map<Coin>(recivedCoin);
            var insertCoin = await provider.CreateAsync(coin);
            return mapper.Map<CoinDto>(insertCoin);

        }

        public async Task<IEnumerable<CoinDto>> GetAllCoinsAsync()
        {
            IEnumerable<Coin> coins = await provider.GetAllAsync();
            return mapper.Map<IEnumerable<CoinDto>>(coins);
        }

        public async Task<CoinDto> GetCoinAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Coin coin = await provider.GetAsync(id);

            return mapper.Map<CoinDto>(coin);
        }
    }
}
