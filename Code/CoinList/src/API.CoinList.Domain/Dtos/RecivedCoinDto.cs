using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.CoinList.Domain.Dtos
{
    public readonly record struct RecivedCoinDto
    (
        [property: Required]
        string Symbol,
        [property: Required]
        string Name,
        [property: Range(0, int.MaxValue)]
        decimal Value
    );
}
