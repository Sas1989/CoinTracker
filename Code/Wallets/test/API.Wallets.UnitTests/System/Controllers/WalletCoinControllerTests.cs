using API.Wallets.Controllers;
using API.Wallets.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.UnitTests.System.Controllers
{
    public class WalletCoinControllerTests
    {
        [Test]
        public void PostAsync_Return_Ok()
        {
            var controller = new WalletCoinController();
            var recivedWalletCoinDto = WalletFixture.RecivedWalletCoinDto();

            var ret = controller.PostAsync(recivedWalletCoinDto);

            Assert.That(ret, Is.TypeOf(typeof(OkObjectResult)));
        }
    }
}
