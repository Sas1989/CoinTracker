using CoinTracker.API.SDK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.Wallets.Domain.Entities
{
    public class Wallet : Entity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
