using CoinTracker.API.CoinList.Application.Common.Publishers;
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
        private readonly ICoinPublisher coinPublisher;

        public CoinListController(ICoinService coinService, ICoinPublisher coinPublisher)
        {
            this.coinService = coinService;
            this.coinPublisher = coinPublisher;
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
            
            return CoinResultAsync(coin);
        }
        [HttpGet("symbol/{symbol}")]
        public async Task<IActionResult> GetBySimbolAsync(string symbol)
        {
            var coin = await coinService.GetCoinAsync(symbol);

            return CoinResultAsync(coin);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(RecivedCoinDto coinDto)
        {
            CoinDto coin = await coinService.CreateAsync(coinDto);

            await PublishCreation(coin);

            return CoinResultAsync(coin);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkAsync(IEnumerable<RecivedCoinDto> recivedCoin)
        {
            IEnumerable<CoinDto> coins = await coinService.CreateMultipleAsync(recivedCoin);

            await PublishCreation(coins);

            return Ok(coins);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, RecivedCoinDto recivedCoin)
        {
            var coin = await coinService.UpdateCoin(id, recivedCoin);

            await PublishUpdate(coin);

            return CoinResultAsync(coin);
        }

        [HttpPut("symbol/{symbol}")]
        public async Task<IActionResult> PutBySymbolAsync(string symbol, RecivedCoinDto recivedCoin)
        {
            var coin = await coinService.UpdateCoin(symbol, recivedCoin);

            await PublishUpdate(coin);

            return CoinResultAsync(coin);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await coinService.DeleteCoin(id))
            {
                await PublishDelete(id);
                return Ok();
            }

            return NotFound();
        }

        private IActionResult CoinResultAsync(CoinDto coin)
        {
            if (coin == null)
            {
                return NotFound();
            }

            return Ok(coin);
        }

        private async Task PublishCreation(CoinDto coin)
        {
            if (coin != null)
            {
                await coinPublisher.PublishCreateAsync(coin);
            }
        }

        private async Task PublishCreation(IEnumerable<CoinDto> coins)
        {
            if (coins != null)
            {
                await coinPublisher.PublishCreateAsync(coins);
            }
        }

        private async Task PublishUpdate(CoinDto coin)
        {
            if (coin != null)
            {
                await coinPublisher.PublishUpdateAsync(coin);
            }
        }

        private async Task PublishDelete(Guid id)
        {
            await coinPublisher.PublishDeleteAsync(id);
        }
    }
}

