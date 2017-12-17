//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Dtos;

namespace Economy.Dtos
{
    public class CurrencyTypeDto : BaseDto
    {
        /// <summary>
        ///наименование валюты
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///трехбуквенное обозначение
        /// </summary>
        public virtual string ShortName { get; set; }

        /// <summary>
        ///валюта
        /// </summary>
        public virtual List<CourseArhiveDto> CourseArhives { get; set; }

        /// <summary>
        ///валюта транзакции
        /// </summary>
        public virtual List<TransactionDto> Transactions { get; set; }

        /// <summary>
        ///валюта кошелька
        /// </summary>
        public virtual List<WalletDto> Wallets { get; set; }

    
    }
}