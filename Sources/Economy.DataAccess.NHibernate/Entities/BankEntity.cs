using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace Economy.DataAccess.NHibernate.Entities
{
    public class BankEntity : BaseEntity
    {
        /// <summary>
        ///идентификатор
        /// </summary>
        [NotNull]
        public virtual int Id { get; set; }

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
        ///Банк
        /// </summary>
        public virtual List<CourseArhiveEntity> CourseArhives { get; set; }


    }
}