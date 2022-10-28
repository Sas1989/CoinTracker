using CoinTracker.API.CoinList.Application.Services.Interfaces;
using CoinTracker.API.CoinList.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CoinTracker.API.CoinList.Controllers.Controllers
{
    [ApiController]
    [Route("api/coin")]
    public class CoinListController : ControllerBase
    {
        private readonly ICoinService coinService;

        public CoinListController(ICoinService coinService)
        {
            this.coinService = coinService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            var result = await coinService.GetAllCoinsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            CoinDto coin = await coinService.GetCoinAsync(id);
            
            return CoinResult(coin);
        }
        [HttpGet("symbol/{symbol}")]
        public async Task<IActionResult> GetBySimbolAsync(string symbol)
        {
            var coin = await coinService.GetCoinAsync(symbol);

            return CoinResult(coin);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(RecivedCoinDto coinDto)
        {
            CoinDto coin = await coinService.CreateAsync(coinDto);
            return CoinResult(coin);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkAsync(IEnumerable<RecivedCoinDto> recivedCoin)
        {
            IEnumerable<CoinDto> coins = await coinService.CreateMultipleAsync(recivedCoin);
            return Ok(coins);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, RecivedCoinDto recivedCoin)
        {
            var coin = await coinService.UpdateCoin(id, recivedCoin);

            return CoinResult(coin);
        }

        [HttpPut("symbol/{symbol}")]
        public async Task<IActionResult> PutBySymbolAsync(string symbol, RecivedCoinDto recivedCoin)
        {
            var coin = await coinService.UpdateCoin(symbol, recivedCoin);

            return CoinResult(coin);
        }

        private IActionResult CoinResult(CoinDto coin)
        {
            if (coin == null)
            {
                return NotFound();
            }

            return Ok(coin);
        }
    }
}

