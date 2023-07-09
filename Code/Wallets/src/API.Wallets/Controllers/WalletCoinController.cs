using API.Wallets.Application.Services;
using API.Wallets.Domain.Dtos.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace API.Wallets.Controllers
{
    [ApiController]
    [Route("api/{id}/wallet")]
    public class WalletCoinController : ControllerBase
    {
        private readonly IWalletService walletService;

        public WalletCoinController(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Guid id, WalletCoinDtoInput recivedWalletCoinDto)
        {
            var wallet = await walletService.AddCoin(id, recivedWalletCoinDto);
            
            if(wallet == default)
                return NotFound();
            
            return Ok(wallet);

        }
    }
}
