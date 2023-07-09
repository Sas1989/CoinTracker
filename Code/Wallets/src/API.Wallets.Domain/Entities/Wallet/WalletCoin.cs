using API.SDK.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Domain.Entities.Wallet
{
    public class WalletCoin
    {
        public Guid Coin_Id { get; set; }
        public decimal NumberOfCoin { get; set; }
    }
}
