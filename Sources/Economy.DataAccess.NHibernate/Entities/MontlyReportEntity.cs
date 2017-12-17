using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace Economy.DataAccess.NHibernate.Entities
{
    public class MontlyReportEntity : BaseEntity
    {
        /// <summary>
        ///идентификатор
        /// </summary>
        [NotNull]
        public virtual Guid Id { get; set; }

        /// <summary>
        ///баланс на начало месяца
        /// </summary>
        [NotNull]
        public virtual decimal StartBalance { get; set; }

        /// <summary>
        ///баланс на конец месяца
        /// </summary>
        [NotNull]
        public virtual decimal EndBalance { get; set; }

        /// <summary>
        ///Период действия (год месяц)
        /// </summary>
        [NotNull]
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        ///Идентификатор кошелька
        /// </summary>
        [NotNull]
        public virtual Guid WalletId { get; set; }

        public virtual WalletEntity Wallet { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        [NotNull]
        public virtual int Version { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual List<TransactionEntity> Transactions { get; set; }


    }
}