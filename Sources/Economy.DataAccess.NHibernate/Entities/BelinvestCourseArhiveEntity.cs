using System;

namespace Economy.DataAccess.NHibernate.Entities
{
    public class BelinvestCourseArhiveEntity : BaseEntity
    {
        /// <summary>
        /// Дата записи
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public CurrencyEntity Currency { get; set; }

        /// <summary>
        /// Цена покупки
        /// </summary>
        public decimal Buy { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        public decimal Sel { get; set; }
    }
}