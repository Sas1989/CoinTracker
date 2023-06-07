using API.CoinList.Application.Common.Publishers;
using API.CoinList.Domain.Dtos;
using API.Contracts.Coin;
using API.SDK.Application.DataMapper;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.CoinList.Infrastructure.Publishers
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
