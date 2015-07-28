using System;

namespace Economy.Models
{

    [Serializable]
    public class PriceHistory 
    {
        /// <summary>
        /// Дата записи
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public string Currency { get; set; }

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
