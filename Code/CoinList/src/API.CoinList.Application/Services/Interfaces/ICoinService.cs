using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.Domain.Entities;
using CoinTracker.API.SDK.Application.ApplicationService.Interfaces;

namespace CoinTracker.API.CoinList.Application.Services.Interfaces
{
    public interface ICoinService : IApplicationService<Coin, CoinDto, RecivedCoinDto>
    {
        Task<CoinDto> GetAsync(string symbol);
        Task<CoinDto> UpdateAsync(string symbol, RecivedCoinDto recivedCoin);
    }
}
