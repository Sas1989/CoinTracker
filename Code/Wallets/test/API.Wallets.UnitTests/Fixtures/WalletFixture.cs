using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Wallets.Domain.Entities.Wallet;
using API.Wallets.Domain.Dtos.Wallet;
using API.UnitTest.Utility.FixtureManager;

namespace API.Wallets.UnitTests.Fixtures
{
    public static class WalletFixture
    {
        private static FixureManger fixtureManager = new FixureManger();

        public static RecivedWalletDto RecivedWalletDto()
        {
            return fixtureManager.Create<RecivedWalletDto>();
        }

        public static WalletDto WalletDto()
        {
            return fixtureManager.Create<WalletDto>();
        }

        public static Wallet Wallet()
        {
            return fixtureManager.Create<Wallet>();
        }

        public static IEnumerable<WalletDto> WalletDtoList()
        {
            return fixtureManager.CreateList<WalletDto>();
        }

        public static IEnumerable<Wallet> WalletList()
        {
            return fixtureManager.CreateList<Wallet>();

        }

        public static IEnumerable<RecivedWalletDto> RecivedWalletList()
        {
            return fixtureManager.CreateList<RecivedWalletDto>();
        }

        public static RecivedWalletCoinDto RecivedWalletCoinDto()
        {
            return fixtureManager.Create<RecivedWalletCoinDto>();
        }
    }
}
