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
using CoinTracker.API.SDK.Application.ApplicationService;
using CoinTracker.API.SDK.UnitTests.System.Application.ApplicationService;

namespace CoinTracker.API.CoinList.Application.Services
{
    public class CoinService : ApplicationService<Coin, CoinDto, RecivedCoinDto>, ICoinService
    {
        public CoinService(IProvider<Coin> provider, IDataMapper mapper) : base(provider, mapper)
        {
        }

        public async Task<CoinDto> GetAsync(string symbol)
        {
            CheckSymbol(symbol);

            var coinList = await provider.GetAsync(nameof(Coin.Symbol), symbol);

            CheckNoCoinFound(coinList);

            return ToDto(coinList.First());
        }

        public async Task<CoinDto> UpdateAsync(string symbol, RecivedCoinDto recivedCoin)
        {
            CheckSymbol(symbol);

            var coinList = await provider.GetAsync(nameof(Coin.Symbol), symbol);

            CheckNoCoinFound(coinList);

            var coin = ToEntity(recivedCoin);
            coin.Id = coinList.First().Id;

            var coinUpdated = await provider.UpdateAsync(coin);

            return ToDto(coinUpdated);
        }

        private static void CheckNoCoinFound(IEnumerable<Coin> coinList)
        {
            if (!coinList.Any())
            {
                throw new EntityNotFoundException();
            }
        }

        private static void CheckSymbol(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(nameof(symbol));
            }
        }
    }
}
