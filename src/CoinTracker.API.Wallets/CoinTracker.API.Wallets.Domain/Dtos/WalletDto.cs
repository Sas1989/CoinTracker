﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTracker.API.Wallets.Domain.Dtos
{
    public readonly record struct WalletDto(
        Guid Id,
        string Name,
        string Description
     );
}
