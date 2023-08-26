using API.SDK.Domain.Entities;

namespace API.Wallets.Domain.ErrorCodes;

internal static class ErrorCode
{
    internal static class Wallet
    {
        internal static readonly Error NameIsEmpty = new("Wallet.Name.IsEmpty", "Wallet Name Cannot Be Empty");
        internal static readonly Error QuantityIsNegative = new("Wallet.Quantity.IsNegative", "Wallet Quantity Cannot Be Negative");
        internal static readonly Error NameIsAlreadyUsed = new("Wallet.Name.IsAlreadyUsed", "Wallet Name Is Already used");
        internal static readonly Error CoinAlreadyInTheWallet = new("Wallet.Coin.AlreadyInTheWallet", "Wallet cointains already the coin");
    }
    
    internal static class Coin
    {
        internal static readonly Error SimbolIsEmpty = new("Coin.Simbol.IsEmpty", "Coin Symbol Cannot Be Empty");
        internal static readonly Error NameIsEmpty = new("Coin.Name.IsEmpty", "Coin Name Cannot Be Empty");
        internal static readonly Error ValueIsNegative = new("Coin.Value.IsNegative", "Coin Value Cannot Be Negative");
        internal static readonly Error NotFound = new("Coin.NotFound", "Coin Not Found");

    }
    
    
}
