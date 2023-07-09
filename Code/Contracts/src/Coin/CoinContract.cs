﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Contracts.Coin
{
    public class CoinContract
    {
        public Guid Id { get; set; }
        public string? Symbol { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; } = 0;
    }
}
