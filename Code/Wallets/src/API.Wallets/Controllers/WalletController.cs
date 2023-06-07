using API.SDK.Domain.Exceptions;
using API.Wallets.Application.Services.Interfaces;
using API.Wallets.Domain.Dtos.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace API.Wallets.Controllers
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
            try
            {
                var wallet = await walletServices.GetAsync(id);
                return Ok(wallet);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> PutAsync(Guid id, RecivedWalletDto recivedWallet)
        {
            try
            {
                var wallet = await walletServices.UpdateAsync(id, recivedWallet);
                return Ok(wallet);
            }
            catch (EntityNotFoundException)
            {

                return NotFound();
            }

        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkAsync(IEnumerable<RecivedWalletDto> recivedWallets)
        {
            IEnumerable<WalletDto> wallets = await walletServices.CreateMultipleAsync(recivedWallets);

            return Ok(wallets);
        }
    }
}