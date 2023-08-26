using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Application.WalletFeature.Dtos
{
    public record WalletDto
    {
        public Guid Id;
        public string Name = string.Empty;
        public string? Description;
        public List<WalletCoinDto> Coins = new();

        public record WalletCoinDto
        {
            public Guid CoinId;
            public string Symbol = string.Empty;
            public string Name = string.Empty;
            public decimal Quantity = 0;
        }
    }
}
