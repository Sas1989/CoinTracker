using CoinTracker.API.Wallets.Domain.Dtos;
using CoinTracker.API.Wallets.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.Wallets.UnitTests.Fixture
{
    public static class WalletFixture
    {
        private static List<Guid> Ids = new List<Guid> { Guid.Parse("3b0f7776-ea82-4176-9756-a0347dd4b9b1"), Guid.Parse("416f0448-cee8-4d31-b609-09fae01e116e") };
        private static List<string> Names = new List<string> { "TestWallet", "SeondWallet" };
        private static List<string> Descriptions = new List<string> { "This is a test wallet", "Second Walle Description" };

        public static RecivedWalletDto RecivedWalletDto()
        {
            return new RecivedWalletDto { Name = Names[0], Description = Descriptions[0] };
        }
        
        public static WalletDto WalletDto()
        {
            return new WalletDto { Id = Ids[0],  Name = Names[0], Description = Descriptions[0] };
        }

        public static Wallet Wallet()
        {
            return new Wallet { Id = Ids[0], Name = Names[0], Description = Descriptions[0] };
        }

        public static IEnumerable<WalletDto> WalletDtoList()
        {
            IEnumerable<WalletDto> walletList = new List<WalletDto>();
            for(int i = 0; i < Ids.Count; i++)
            {
                walletList.Append(new WalletDto { Id = Ids[i], Name = Names[i], Description = Descriptions[i] });
            }
            return walletList;
        }

        public static IEnumerable<Wallet> WalletList()
        {
            IEnumerable<Wallet> walletList = new List<Wallet>();
            for (int i = 0; i < Ids.Count; i++)
            {
                walletList.Append(new Wallet { Id = Ids[i], Name = Names[i], Description = Descriptions[i] });
            }
            return walletList;
        }
    }
}
