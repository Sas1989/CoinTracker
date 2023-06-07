using API.CoinList.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.CoinList.Application.Common.Publishers
{
    public interface ICoinPublisher
    {
        Task PublishCreateAsync(CoinDto coinDto);
        Task PublishCreateAsync(IEnumerable<CoinDto> coinDtoList);
        Task PublishUpdateAsync(CoinDto coinDto);
        Task PublishDeleteAsync(Guid newId);
    }
}
