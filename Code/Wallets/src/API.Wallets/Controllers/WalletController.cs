using API.Wallets.Application;
using API.Wallets.Application.WalletCommands.CreateCommand;
using API.Wallets.Application.WalletFeature.CreateCommand;
using Microsoft.AspNetCore.Mvc;

namespace API.Wallets.Controllers;

[ApiController]
[Route("api/wallet")]
public class WalletController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public WalletController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(WalletDtoInput inputDto)
    {
        var command = new CreateWalletCommand { Name = inputDto.Name, Description = inputDto.Description };
        var result = await _dispatcher.Send(command);

        return Ok(result);
    }

    public record WalletDtoInput(string Name, string Description);


    /*
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var wallet = await walletServices.GetAsync(id);

        return ReturnAction(wallet);

    }

    private IActionResult ReturnAction(WalletDto wallet)
    {
        if (wallet == default)
            return NotFound();

        return Ok(wallet);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (await walletServices.DeleteAsync(id))
        {
            return Ok();
        }

        return NotFound();

    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var wallets = await walletServices.GetAllAsync();
        return Ok(wallets);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, WalletDtoInput recivedWallet)
    {

        var wallet = await walletServices.UpdateAsync(id, recivedWallet);
        return ReturnAction(wallet);

    }

    [HttpPost("bulk")]
    public async Task<IActionResult> BulkAsync(IEnumerable<WalletDtoInput> recivedWallets)
    {
        IEnumerable<WalletDto> wallets = await walletServices.CreateMultipleAsync(recivedWallets);

        return Ok(wallets);
    }*/
}