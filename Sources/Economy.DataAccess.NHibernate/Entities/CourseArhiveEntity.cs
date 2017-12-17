using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace Economy.DataAccess.NHibernate.Entities
{
    public class CourseArhiveEntity : BaseEntity
    {
        /// <summary>
        ///валюта
        /// </summary>
        [NotNull]
        public virtual int CurrencyTypeId { get; set; }

        public virtual CurrencyTypeEntity CurrencyType { get; set; }

        /// <summary>
        ///дата
        /// </summary>
        [NotNull]
        public virtual DateTime RegDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [NotNull]
        public virtual int CurrencyTypeBaseId { get; set; }

        public virtual CurrencyTypeEntity CurrencyTypeBase { get; set; }

        /// <summary>
        ///Банк
        /// </summary>
        [NotNull]
        public virtual int BankId { get; set; }

        public virtual BankEntity Bank { get; set; }

        /// <summary>
        ///цена покупки
        /// </summary>
        [NotNull]
        public virtual decimal Buy { get; set; }

        /// <summary>
        ///цена продажи
        /// </summary>
        [NotNull]
        public virtual decimal Sel { get; set; }

        /// <summary>
        ///Идентификатор
        /// </summary>
        [NotNull]
        public virtual int Id { get; set; }

        /// <summary>
        ///Версия для оптимистической блокировки
        /// </summary>
        [NotNull]
        public virtual int Version { get; set; }


    }
}