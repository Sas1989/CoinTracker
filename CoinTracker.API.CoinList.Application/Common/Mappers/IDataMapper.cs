using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.CoinList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Application.Common.Mappers
{
    public interface IDataMapper
    {
        TDestination Map<TDestination>(object sourceObj);
    }
}
