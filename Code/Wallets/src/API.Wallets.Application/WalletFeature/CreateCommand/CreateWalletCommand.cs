using API.SDK.Application.Dispatcher;
using API.Wallets.Application.WalletFeature.Dtos;

namespace API.Wallets.Application.WalletFeature.CreateCommand;

public class CreateWalletCommand : ICommand<WalletDto>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
