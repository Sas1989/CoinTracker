using MassTransit;
using API.Wallets.Domain.Dtos.Coin;
using API.SDK.Application.DataMapper;
using API.Contracts.Coin;
using API.Wallets.Application.Services;

namespace API.Wallets.Consumers
{
    public class CoinInserConsumer : IConsumer<CoinInsert>
    {
        private readonly ICoinService coinService;
        private readonly IDataMapper dataMapper;

        public CoinInserConsumer(ICoinService coinService, IDataMapper dataMapper)
        {
            this.coinService = coinService;
            this.dataMapper = dataMapper;
        }

        public async Task Consume(ConsumeContext<CoinInsert> context)
        {
            var coinContract = context.Message;
            var coinDto = dataMapper.Map<CoinDto>(coinContract);

            await coinService.CreateAsync(coinDto);
        }
    }
}
