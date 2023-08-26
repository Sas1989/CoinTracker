using API.SDK.Domain.Entities;
using API.SDK.Domain.Exceptions;
using API.Wallets.Domain.ErrorCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Domain.Exception
{
    public class CoinAlreadyInTheWallet : BaseApplicationException
    {
        public CoinAlreadyInTheWallet() : base(ErrorCode.Wallet.CoinAlreadyInTheWallet)
        {
        }
    }
}
