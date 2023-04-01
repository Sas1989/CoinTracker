using CoinTracker.API.CoinList.Acceptance.Support.Services.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.CoinList.Acceptance.Support.Wallets.Models
{
    public class Wallet : BaseEntity, IEquatable<Wallet?>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Wallet);
        }

        public bool Equals(Wallet? other)
        {
            return other != null &&
                   Id.Equals(other.Id) &&
                   Name == other.Name &&
                   Description == other.Description;
        }

        public static bool operator ==(Wallet? left, Wallet? right)
        {
            return EqualityComparer<Wallet>.Default.Equals(left, right);
        }

        public static bool operator !=(Wallet? left, Wallet? right)
        {
            return !(left == right);
        }

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
