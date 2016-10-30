using System;
using System.Collections.Generic;
using CQRS.Common;
using CQRS.Dtos;

namespace Economy.Dtos
{
    public class CourseArhiveDto : BaseDto
    {
        /// <summary>
        ///валюта
        /// </summary>
        public virtual int CurrencyTypeId { get; set; }

        public virtual CurrencyTypeDto CurrencyType { get; set; }

        /// <summary>
        ///дата
        /// </summary>
        public virtual DateTime RegDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual int CurrencyTypeBaseId { get; set; }

        public virtual CurrencyTypeDto CurrencyTypeBase { get; set; }

        /// <summary>
        ///Банк
        /// </summary>
        public virtual int BankId { get; set; }

        public virtual BankDto Bank { get; set; }

        /// <summary>
        ///цена покупки
        /// </summary>
        public virtual decimal Buy { get; set; }

        /// <summary>
        ///цена продажи
        /// </summary>
        public virtual decimal Sel { get; set; }

    
    }
}