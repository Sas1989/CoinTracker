using API.UnitTest.Utility.TestDataMapper;
using API.Wallets.Domain.Entities.CoinEntity;
using API.Wallets.Domain.Entities.WalletEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.UnitTests.DataMapper
{
    internal class WalletDataMapper : TestDataMapper
    {
        protected override void LoadConfiguration()
        {
            AddMapping<WalletCoin, Coin>();
        }
    }
}
