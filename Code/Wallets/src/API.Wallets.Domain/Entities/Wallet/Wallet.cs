﻿using API.SDK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Domain.Entities.Wallet
{
    public class Wallet : Entity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<WalletCoin> Coins { get; set; } = new List<WalletCoin>();

    }
}
