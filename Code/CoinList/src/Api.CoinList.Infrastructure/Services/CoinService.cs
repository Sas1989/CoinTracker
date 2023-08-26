using API.CoinList.Domain.Entities;
using API.CoinList.Domain.Dtos;
using API.SDK.Application.DataMapper;
using API.SDK.Application.Repository;
using API.SDK.Infrastructure.Services;
using API.CoinList.Application.Services;

namespace API.CoinList.Infrastructure.Services
{
    public class CoinService : ApplicationService<Coin, CoinDto, CoinDtoInput>, ICoinService
    {
        public CoinService(IRepository<Coin> repository, IDataMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<CoinDto> GetAsync(string symbol)
        {
            CheckSymbol(symbol);

            var coinList = await repository.GetAsync(nameof(Coin.Symbol), symbol);

            if (coinList == null || coinList.Any()) 
            {
                return default;
            }

            return ToDto(coinList.FirstOrDefault());
        }

        public async Task<CoinDto> UpdateAsync(string symbol, CoinDtoInput recivedCoin)
        {
            CheckSymbol(symbol);

            var coinList = await repository.GetAsync(nameof(Coin.Symbol), symbol);

            if (coinList.Any())
            {
                return default;
            }

            var coin = ToEntity(recivedCoin);
            coin.Id = coinList.First().Id;

            var coinUpdated = await repository.UpdateAsync(coin);

            return ToDto(coinUpdated);
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
