using System;
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Dtos;

namespace Economy.Dtos
{
    public class WalletDto : BaseDto
    {
        /// <summary>
        ///идентификатор
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///первоначальный баланс счета
        /// </summary>
        public virtual decimal? StartBalance { get; set; }

        /// <summary>
        ///итоговое состояние счета
        /// </summary>
        public virtual decimal? Balance { get; set; }

        /// <summary>
        ///пользователь
        /// </summary>
        public virtual Guid SystemUserId { get; set; }

        public virtual SystemUserDto SystemUser { get; set; }

        /// <summary>
        ///валюта кошелька
        /// </summary>
        public virtual int CurrencyTypeId { get; set; }

        public virtual CurrencyTypeDto CurrencyType { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        ///Идентификатор кошелька
        /// </summary>
        public virtual List<MontlyReportDto> MontlyReports { get; set; }

    
    }
}