using API.CoinList.Domain.Dtos;
using API.CoinList.Domain.Entities;
using API.SDK.Application.ApplicationService.Interfaces;

namespace API.CoinList.Application.Services.Interfaces
{
    public interface ICoinService : IApplicationService<Coin, CoinDto, RecivedCoinDto>
    {
        Task<CoinDto> GetAsync(string symbol);
        Task<CoinDto> UpdateAsync(string symbol, RecivedCoinDto recivedCoin);
    }
}
