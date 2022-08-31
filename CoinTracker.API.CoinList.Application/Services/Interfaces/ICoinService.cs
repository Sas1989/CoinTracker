﻿using CoinTracker.API.CoinList.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Application.Services.Interfaces
{
    public interface ICoinService
    {
        Task<IEnumerable<CoinDto>> GetAllCoinsAsync();
        Task<CoinDto> GetCoinAsync(Guid id);
        Task<CoinDto> CreateAsync(RecivedCoinDto recivedCoin);
    }
}
