using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.Models
{
    public class Coin : IEquatable<Coin?>
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Coin);
        }

        public bool Equals(Coin? other)
        {
            return other != null &&
                   Id.Equals(other.Id) &&
                   Symbol == other.Symbol &&
                   Name == other.Name &&
                   Value == other.Value;
        }
    }

    public class RecivedCoin 
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }

    }
}
