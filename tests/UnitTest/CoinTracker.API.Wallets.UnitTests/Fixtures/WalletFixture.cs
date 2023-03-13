using CoinTracker.API.Wallets.Domain.Dtos;
using CoinTracker.API.Wallets.Domain.Entities;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinTracker.API.UnitTest.Utiltiy.FixtureManager;

namespace CoinTracker.API.Wallets.UnitTests.Fixtures
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
    }
}
