using CoinTracker.API.CoinList.Application.Common.Publishers;
using CoinTracker.API.CoinList.Application.Services.Interfaces;
using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.SDK.UnitTests.System.Application.ApplicationService;
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

            var result = await coinService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                CoinDto coin = await coinService.GetAsync(id);
                return Ok(coin);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

        }
        [HttpGet("symbol/{symbol}")]
        public async Task<IActionResult> GetBySimbolAsync(string symbol)
        {
            try
            {
                var coin = await coinService.GetAsync(symbol);
                return Ok(coin);
            }
            catch (EntityNotFoundException)
            {

                return NotFound();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(RecivedCoinDto coinDto)
        {
            CoinDto coin = await coinService.CreateAsync(coinDto);

            await coinPublisher.PublishCreateAsync(coin);

            return Ok(coin);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkAsync(IEnumerable<RecivedCoinDto> recivedCoin)
        {
            IEnumerable<CoinDto> coins = await coinService.CreateMultipleAsync(recivedCoin);

            await coinPublisher.PublishCreateAsync(coins);

            return Ok(coins);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, RecivedCoinDto recivedCoin)
        {
            try
            {
                var coin = await coinService.UpdateAsync(id, recivedCoin);
                await coinPublisher.PublishUpdateAsync(coin);
                return Ok(coin);
            }
            catch (EntityNotFoundException)
            {

                return NotFound();
            }

        }

        [HttpPut("symbol/{symbol}")]
        public async Task<IActionResult> PutBySymbolAsync(string symbol, RecivedCoinDto recivedCoin)
        {
            try
            {
                var coin = await coinService.UpdateAsync(symbol, recivedCoin);
                await coinPublisher.PublishUpdateAsync(coin);
                return Ok(coin);
            }
            catch (EntityNotFoundException)
            {

                return NotFound();
            }

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
    }
}

