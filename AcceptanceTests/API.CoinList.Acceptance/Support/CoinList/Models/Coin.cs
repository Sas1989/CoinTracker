using CoinTracker.API.CoinList.Acceptance.Support.Services.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.CoinList.Models
{
    public class Coin : BaseEntity, IEquatable<Coin?>
    {
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

        public static explicit operator RecivedCoin(Coin obj)
        {
            RecivedCoin output = new RecivedCoin
            {
                Symbol = obj.Symbol,
                Name = obj.Name,
                Value = obj.Value
            };
            return output;
        }
    }
}
