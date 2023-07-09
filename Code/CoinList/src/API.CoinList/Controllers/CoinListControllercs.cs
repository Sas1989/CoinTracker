using API.CoinList.Application.Common.Publishers;
using API.CoinList.Application.Services;
using API.CoinList.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.CoinList.Controllers
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

            var result = await coinService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            CoinDto coin = await coinService.GetAsync(id);

            return ReturnCoin(coin);

        }
        [HttpGet("symbol/{symbol}")]
        public async Task<IActionResult> GetBySimbolAsync(string symbol)
        {
            var coin = await coinService.GetAsync(symbol);
            return ReturnCoin(coin);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CoinDtoInput coinDto)
        {
            CoinDto coin = await coinService.CreateAsync(coinDto);

            await coinPublisher.PublishCreateAsync(coin);

            return Ok(coin);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkAsync(IEnumerable<CoinDtoInput> recivedCoin)
        {
            IEnumerable<CoinDto> coins = await coinService.CreateMultipleAsync(recivedCoin);

            await coinPublisher.PublishCreateAsync(coins);

            return Ok(coins);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, CoinDtoInput recivedCoin)
        {
            var coin = await coinService.UpdateAsync(id, recivedCoin);

            return await ReturnCoinAndPublish(coin);
        }

        [HttpPut("symbol/{symbol}")]
        public async Task<IActionResult> PutBySymbolAsync(string symbol, CoinDtoInput recivedCoin)
        {
            var coin = await coinService.UpdateAsync(symbol, recivedCoin);

            return await ReturnCoinAndPublish(coin);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await coinService.DeleteAsync(id))
            {
                await coinPublisher.PublishDeleteAsync(id);
                return Ok();
            }

            return NotFound();
        }

        private async Task<IActionResult> ReturnCoinAndPublish(CoinDto coin)
        {
            if (coin == default(CoinDto))
            {
                return NotFound();
            }

            await coinPublisher.PublishUpdateAsync(coin);
            return Ok(coin);
        }

        private IActionResult ReturnCoin(CoinDto coin)
        {
            if (coin == default(CoinDto))
            {
                return NotFound();
            }
            return Ok(coin);
        }
    }
}

