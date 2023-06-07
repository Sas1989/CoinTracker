using API.Contracts.Coin;
using API.SDK.Application.DataMapper;
using API.Wallets.Application.Services.Interfaces;
using API.Wallets.Domain.Dtos.Coin;
using MassTransit;

namespace API.Wallets.Consumers
{
    public class CoinUpdateConsumer : IConsumer<CoinUpdate>
    {
        private readonly ICoinService coinService;
        private readonly IDataMapper dataMapper;

        public CoinUpdateConsumer(ICoinService coinService, IDataMapper dataMapper)
        {
            this.coinService = coinService;
            this.dataMapper = dataMapper;
        }

        public async Task Consume(ConsumeContext<CoinUpdate> context)
        {
            var coinContract = context.Message;
            var coinDto = dataMapper.Map<CoinDto>(coinContract);

            await coinService.UpdateAsync(coinDto.Id, coinDto);
        }
    }
}
