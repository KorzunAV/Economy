//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Dtos;

namespace Economy.Dtos
{
    public class BankDto : BaseDto
    {
        /// <summary>
        ///
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///Банк
        /// </summary>
        public virtual List<CourseArhiveDto> CourseArhives { get; set; }

        /// <summary>
        ///--Привязка к банку
        /// </summary>
        public virtual List<WalletDto> Wallets { get; set; }

    
    }
}