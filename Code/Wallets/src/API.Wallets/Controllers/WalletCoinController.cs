using Microsoft.AspNetCore.Mvc;

namespace API.Wallets.Controllers
{
    [ApiController]
    [Route("api/{id}/wallet")]
    public class WalletCoinController : ControllerBase
    {
 /*       
        private readonly IWalletService walletService;
        private readonly IDispatcher dispatcher;

        public WalletCoinController(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Guid id, WalletCoinDtoInput recivedWalletCoinDto)
        {
            var request = new AddCoinToWalletRequest
            {
                WalletId = id,
                CoinId = recivedWalletCoinDto.Coin_ID,
                Quantity = recivedWalletCoinDto.NumberOfCoin
            };

            WalletDto ciao = await dispatcher.Send<WalletDto>(request);

            return ciao;
            var wallet = await walletService.AddCoin(id, recivedWalletCoinDto);
            
            if(wallet == default)
                return NotFound();
            
            return Ok(wallet);

        }*/
    }
}
