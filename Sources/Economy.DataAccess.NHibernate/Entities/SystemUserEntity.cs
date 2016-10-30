using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace Economy.DataAccess.NHibernate.Entities
{
    public class SystemUserEntity : BaseEntity
    {
        /// <summary>
        ///идентификатор пользователя
        /// </summary>
        [NotNull]
        public virtual Guid Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        [NotNull]
        public virtual int Version { get; set; }

        /// <summary>
        ///пользователь
        /// </summary>
        public virtual List<WalletEntity> Wallets { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is SystemUserEntity)
            {
                var typed = (SystemUserEntity)obj;
                if (typed.Id != Id)
                    return false;
                return true;
            }
            return false;
        }
    }
}