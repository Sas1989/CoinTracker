using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.Models
{
    public class Coin
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    public class RecivedCoin 
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }

    }
}
