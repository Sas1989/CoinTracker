using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Domain.Dtos
{
    public readonly record struct CoinDto
    (
        Guid Id,
        string Symbol,
        string Name,
        decimal Value
    );
}
