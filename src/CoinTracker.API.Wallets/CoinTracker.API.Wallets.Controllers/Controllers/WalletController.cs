
using CoinTracker.API.Wallets.Application.Services.Interfaces;
using CoinTracker.API.Wallets.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CoinTracker.API.Wallets.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService walletServices;

        public WalletController(IWalletService walletServices)
        {
            this.walletServices = walletServices;
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(RecivedWalletDto recivedWallet)
        {
            var wallet = await walletServices.CreateAsync(recivedWallet);

            return Ok(wallet);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var wallet = await walletServices.GetWalletAsync(id);
            if(wallet == null)
            {
                return NotFound();
            }

            return Ok(wallet);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if(await walletServices.DeleteWalletAsync(id))
            {
                return Ok();
            }

            return NotFound();

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var wallets = await walletServices.GetWalletAsync();
            return Ok(wallets);
        }
    }
}