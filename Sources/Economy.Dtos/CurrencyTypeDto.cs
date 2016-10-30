using System;
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Dtos;

namespace Economy.Dtos
{
    public class CurrencyTypeDto : BaseDto
    {
        /// <summary>
        ///идентификатор
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///наименование валюты
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///трехбуквенное обозначение
        /// </summary>
        public virtual string ShortName { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        ///валюта
        /// </summary>
        public virtual List<CourseArhiveDto> CourseArhives { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual List<CourseArhiveDto> CourseArhiveBases { get; set; }

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