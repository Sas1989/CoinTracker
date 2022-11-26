using CoinTracker.API.CoinList.Application.Common.Mappers;
using CoinTracker.API.CoinList.Application.Common.Publishers;
using CoinTracker.API.CoinList.Domain.Dtos;
using CoinTracker.API.Contracts.Coin;
using CoinTracker.API.Contracts.Coins;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.Api.CoinList.Infrastructure.Publishers
{
    public class RabbitMqPublisher : ICoinPublisher
    {
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IDataMapper mapper;

        public RabbitMqPublisher(IPublishEndpoint publishEndpoint, IDataMapper mapper)
        {
            this.publishEndpoint = publishEndpoint;
            this.mapper = mapper;
        }
        public async Task PublishCreateAsync(CoinDto coinDto)
        {
            var coin = mapper.Map<CoinInsert>(coinDto);
            await publishEndpoint.Publish(coin);
        }

        public async Task PublishCreateAsync(IEnumerable<CoinDto> coinDtoList)
        {
            var coins = mapper.Map<IEnumerable<CoinInsert>>(coinDtoList);
            await publishEndpoint.PublishBatch(coins);
        }

        public async Task PublishUpdateAsync(CoinDto coinDto)
        {
            var coin = mapper.Map<CoinUpdate>(coinDto);
            await publishEndpoint.Publish(coin);
        }

        public async Task PublishDeleteAsync(Guid coinId)
        {
            var coin = new CoinDelete { Id = coinId };
            await publishEndpoint.Publish(coin);
        }
    }
}
