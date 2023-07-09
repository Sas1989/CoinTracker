﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Wallets.Domain.Dtos.Wallet
{
    public readonly record struct WalletDtoInput(
        string Name,
        string Description);
}