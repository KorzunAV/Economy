using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace Economy.DataAccess.NHibernate.Entities
{
    public class CurrencyTypeEntity : BaseEntity
    {
        /// <summary>
        ///идентификатор
        /// </summary>
        [NotNull]
        public virtual int Id { get; set; }

        /// <summary>
        ///наименование валюты
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///трехбуквенное обозначение
        /// </summary>
        [NotNull]
        [NotEmpty]
        [Length(3)] 
        public virtual string ShortName { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        [NotNull]
        public virtual int Version { get; set; }

        /// <summary>
        ///валюта
        /// </summary>
        public virtual List<CourseArhiveEntity> CourseArhives { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual List<CourseArhiveEntity> CourseArhiveBases { get; set; }

        /// <summary>
        ///валюта транзакции
        /// </summary>
        public virtual List<TransactionEntity> Transactions { get; set; }

        /// <summary>
        ///валюта кошелька
        /// </summary>
        public virtual List<WalletEntity> Wallets { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is CurrencyTypeEntity)
            {
                var typed = (CurrencyTypeEntity)obj;
                if (typed.Id != Id)
                    return false;
                return true;
            }
            return false;
        }
    }
}