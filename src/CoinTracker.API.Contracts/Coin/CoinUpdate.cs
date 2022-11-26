using CoinTracker.API.Contracts.Coins;

namespace CoinTracker.API.Contracts.Coins
{
    public class CoinUpdate : CoinContract
    {
        public Guid Id { get; set; }
    }
}