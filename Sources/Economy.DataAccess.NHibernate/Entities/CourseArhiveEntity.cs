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

        public override bool Equals(object obj)
        {
            if (obj is CourseArhiveEntity)
            {
                var typed = (CourseArhiveEntity)obj;
                if (typed.CurrencyTypeId != CurrencyTypeId && (typed.CurrencyType == null || CurrencyType == null || typed.CurrencyType != CurrencyType))
                    return false;
                if (typed.RegDate != RegDate)
                    return false;
                if (typed.BankId != BankId && (typed.Bank == null || Bank == null || typed.Bank != Bank))
                    return false;
                return true;
            }
            return false;
        }
    }
}