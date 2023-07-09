using API.CoinList.Domain.Dtos;
using API.CoinList.Domain.Entities;
using API.SDK.Application.ApplicationService;

namespace API.CoinList.Application.Services
{
    public interface ICoinService : IApplicationService<Coin, CoinDto, CoinDtoInput>
    {
        Task<CoinDto> GetAsync(string symbol);
        Task<CoinDto> UpdateAsync(string symbol, CoinDtoInput recivedCoin);
    }
}
