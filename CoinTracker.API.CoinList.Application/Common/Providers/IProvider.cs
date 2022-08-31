﻿using CoinTracker.API.CoinList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Application.Providers
{
    public interface IProvider
    {
        Task<Coin> CreateAsync(Coin entity);
        Task<IEnumerable<Coin>> GetAllAsync();
        Task<Coin> GetAsync(Guid id);
        Task UpdateAsync(Coin entity);
        Task RemoveAsync(Guid id);
    }
}
