using System;
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Dtos;

namespace Economy.Dtos
{
    public class TransactionDto : BaseDto
    {
        /// <summary>
        ///идентификатор
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        ///дата регистрации транзакции в системе
        /// </summary>
        public virtual DateTime RegistrationDate { get; set; }

        /// <summary>
        ///дата совершения транзакции
        /// </summary>
        public virtual DateTime? TransactionDate { get; set; }

        /// <summary>
        ///код транзакции
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        ///комментарий
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        ///валюта транзакции
        /// </summary>
        public virtual int CurrencyTypeId { get; set; }

        public virtual CurrencyTypeDto CurrencyType { get; set; }

        /// <summary>
        ///сумма в валюте транзакции
        /// </summary>
        public virtual decimal QuantityByTransaction { get; set; }

        /// <summary>
        ///сумма в валюте счета
        /// </summary>
        public virtual decimal? QuantityByWallet { get; set; }

        /// <summary>
        ///комиссия
        /// </summary>
        public virtual decimal? Commission { get; set; }

        /// <summary>
        ///перевод с счета
        /// </summary>
        public virtual Guid FromWalletId { get; set; }

        public virtual WalletDto FromWallet { get; set; }

        /// <summary>
        ///перевод на счет
        /// </summary>
        public virtual Guid ToWalletId { get; set; }

        public virtual WalletDto ToWallet { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual Guid MontlyReportId { get; set; }

        public virtual MontlyReportDto MontlyReport { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        public virtual int Version { get; set; }

    
    }
}