using API.SDK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Domain.Entities
{
    public class Coin : Entity
    {
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty ;
        public decimal Value { get; set; } = 0;
    }
}
