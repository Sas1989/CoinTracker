using API.SDK.Application.DataMapper;
using API.SDK.Application.Dispatcher;
using API.Wallets.Application.WalletFeature.CreateCommand;
using API.Wallets.Application.WalletFeature.Dtos;
using API.Wallets.Domain.Entities.WalletEntity;
using API.Wallets.Domain.Entities.WalletEntity.ValueObjects;
using API.Wallets.Domain.Repositories;

namespace API.Wallets.Application.WalletCommands.CreateCommand;

public class CreateWalletHandler : IRequestHandler<CreateWalletCommand, WalletDto>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IDataMapper _dataMapper;

    public CreateWalletHandler(IWalletRepository walletRepository,IDataMapper dataMapper)
    {
        _walletRepository = walletRepository;
        _dataMapper = dataMapper;
    }

    public async Task<WalletDto> Handle(CreateWalletCommand command)
    {
        var walletName = new WalletName(command.Name);

        var wallet = Wallet.Create(walletName,command.Description);

        await _walletRepository.Save(wallet);

        return _dataMapper.Map<WalletDto>(wallet);
    }

}