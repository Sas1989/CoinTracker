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
            if (coin == null)
            {
                return NotFound();
            }

            return Ok(coin);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(RecivedCoinDto coinDto)
        {
            CoinDto coin = await coinService.CreateAsync(coinDto);
            return Ok(coin);
        }
    }
}
