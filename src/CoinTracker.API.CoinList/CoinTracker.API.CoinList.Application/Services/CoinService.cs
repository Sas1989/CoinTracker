using CoinTracker.API.SDK.Application.IProvider;
using CoinTracker.API.CoinList.Application.Services.Interfaces;
using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinTracker.API.SDK.Application.DataMapper;

namespace CoinTracker.API.CoinList.Application.Services
{
    public class CoinService : ICoinService
    {
        private readonly IProvider<Coin> provider;
        private readonly IDataMapper mapper;

        public CoinService(IProvider<Coin> provider, IDataMapper mapper)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        public async Task<CoinDto> CreateAsync(RecivedCoinDto recivedCoin)
        {
            Coin coin = mapper.Map<Coin>(recivedCoin);
            var insertCoin = await provider.CreateAsync(coin);
            return ToDto(insertCoin);

        }

        public async Task<IEnumerable<CoinDto>> CreateMultipleAsync(IEnumerable<RecivedCoinDto> recivedCoin)
        {
            var coins = mapper.Map<IEnumerable<Coin>>(recivedCoin);
            var insertCoins = await provider.CreateAsync(coins);
            return ToDto(insertCoins);
        }

        public async Task<bool> DeleteCoin(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            return await provider.DeleteAsync(id);
        }

        public async Task<IEnumerable<CoinDto>> GetAllCoinsAsync()
        {
            IEnumerable<Coin> coins = await provider.GetAllAsync();
            return ToDto(coins);
        }

        public async Task<CoinDto> GetCoinAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Coin coin = await provider.GetAsync(id);

            return ToDto(coin);
        }

        public async Task<CoinDto> GetCoinAsync(string symbol)
        {
            if(string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }

            var coinList = await provider.GetAsync(nameof(Coin.Symbol), symbol);
            
            if(coinList.Count() == 0)
            {
                return null;
            }

            return ToDto(coinList.First());
        }

        public async Task<CoinDto> UpdateCoin(Guid id, RecivedCoinDto recivedCoin)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var coin = mapper.Map<Coin>(recivedCoin);
            coin.Id = id;

            var coinUpdated = await provider.UpdateAsync(coin);

            return ToDto(coinUpdated);
        }

        public async Task<CoinDto> UpdateCoin(string symbol, RecivedCoinDto recivedCoin)
        {
            if (string.IsNullOrEmpty(symbol)){
                throw new ArgumentNullException(nameof(symbol));
            }
            
            var coinList = await provider.GetAsync(nameof(Coin.Symbol), symbol);

            if (coinList.Count() == 0)
            {
                return null;
            }

            var coin = mapper.Map<Coin>(recivedCoin);
            coin.Id = coinList.First().Id;

            var coinUpdated = await provider.UpdateAsync(coin);

            return ToDto(coinUpdated);
        }

        private CoinDto ToDto(Coin coin)
        {
            return mapper.Map<CoinDto>(coin);
        }

        private IEnumerable<CoinDto> ToDto(IEnumerable<Coin> coinList)
        {
            return mapper.Map<IEnumerable<CoinDto>>(coinList);
        }
    }
}
