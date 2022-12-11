using CoinTracker.API.CoinList.Acceptance.Support.Services.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.Wallets.Models
{
    public class Wallet : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static explicit operator RecivedWallet(Wallet obj)
        {
            RecivedWallet output = new RecivedWallet
            {
                Name = obj.Name,
                Description = obj.Description,
            };
            return output;
        }

    }

}
