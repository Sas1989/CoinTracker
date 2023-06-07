﻿using API.SDK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Domain.Entities
{
    public class Coin : Entity
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
