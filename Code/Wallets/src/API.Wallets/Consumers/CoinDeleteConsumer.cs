using API.Wallets.Application.Services.Interfaces;
using CoinTracker.API.Contracts.Coin;
using MassTransit;

namespace API.Wallets.Consumers
{
    public class CoinDeleteConsumer : IConsumer<CoinDelete>
    {
        private readonly ICoinService coinService;

        public CoinDeleteConsumer(ICoinService coinService)
        {
            this.coinService = coinService;
        }

        public async Task Consume(ConsumeContext<CoinDelete> context)
        {
            var coinContract = context.Message;
            var id = coinContract.Id;

            await coinService.DeleteAsync(id);
        }
    }
}
