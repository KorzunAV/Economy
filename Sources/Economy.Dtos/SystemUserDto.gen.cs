//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Dtos;

namespace Economy.Dtos
{
    public class SystemUserDto : BaseDto
    {
        /// <summary>
        ///
        /// </summary>
        public virtual string Login { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///пользователь
        /// </summary>
        public virtual List<WalletDto> Wallets { get; set; }

    
    }
}