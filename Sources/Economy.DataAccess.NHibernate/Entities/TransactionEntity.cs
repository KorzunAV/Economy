using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace Economy.DataAccess.NHibernate.Entities
{
    public class TransactionEntity : BaseEntity
    {
        /// <summary>
        ///идентификатор
        /// </summary>
        [NotNull]
        public virtual Guid Id { get; set; }

        /// <summary>
        ///дата регистрации транзакции в системе
        /// </summary>
        [NotNull]
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
        [NotNull]
        public virtual int CurrencyTypeId { get; set; }

        public virtual CurrencyTypeEntity CurrencyType { get; set; }

        /// <summary>
        ///сумма в валюте транзакции
        /// </summary>
        [NotNull]
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

        public virtual WalletEntity FromWallet { get; set; }

        /// <summary>
        ///перевод на счет
        /// </summary>
        [NotNull]
        public virtual Guid ToWalletId { get; set; }

        public virtual WalletEntity ToWallet { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual Guid MontlyReportId { get; set; }

        public virtual MontlyReportEntity MontlyReport { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        [NotNull]
        public virtual int Version { get; set; }


    }
}