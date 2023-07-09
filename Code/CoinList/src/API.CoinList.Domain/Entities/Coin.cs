using API.SDK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.CoinList.Domain.Entities
{
    public class Coin : Entity
    {
        [Required]
        public string Symbol { get; set; } = String.Empty;
        [Required]
        public string Name { get; set; } = String.Empty ;
        public decimal Value { get; set; } = 0;

    }
}
