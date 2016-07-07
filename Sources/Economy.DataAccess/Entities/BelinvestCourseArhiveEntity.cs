using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace Economy.DataAccess.Entities
{
    [TableName(Name = "belinvest_course_arhive")]
    internal class BelinvestCourseArhiveEntity : BaseEntity
    {
        /// <summary>
        /// Дата записи
        /// </summary>
        [MapField("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        [MapField("currency_type")]
        public CurrencyTypeEntity CurrencyTypeId { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public CurrencyTypeEntity CurrencyType { get; set; }

        /// <summary>
        /// Цена покупки
        /// </summary>
        [MapField("buy")]
        public decimal Buy { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        [MapField("sel")]
        public decimal Sel { get; set; }
    }
}