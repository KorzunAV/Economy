using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace Economy.DataAccess.NHibernate.Entities
{
    public class WalletEntity : BaseEntity
    {
        /// <summary>
        ///идентификатор
        /// </summary>
        [NotNull]
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

        public virtual SystemUserEntity SystemUser { get; set; }

        /// <summary>
        ///валюта кошелька
        /// </summary>
        [NotNull]
        public virtual int CurrencyTypeId { get; set; }

        public virtual CurrencyTypeEntity CurrencyType { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        [NotNull]
        public virtual int Version { get; set; }

        /// <summary>
        ///Идентификатор кошелька
        /// </summary>
        public virtual List<MontlyReportEntity> MontlyReports { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is WalletEntity)
            {
                var typed = (WalletEntity)obj;
                if (typed.Id != Id)
                    return false;
                return true;
            }
            return false;
        }
    }
}