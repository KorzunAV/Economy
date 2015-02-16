using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Цена покупки USD
        /// </summary>
        public double UsdBuy { get; set; }

        /// <summary>
        /// Цена продажи USD
        /// </summary>
        public double UsdSel { get; set; }

        /// <summary>
        /// Цена покупки EUR
        /// </summary>
        public double EurBuy { get; set; }

        /// <summary>
        /// Цена продажи EUR
        /// </summary>
        public double EurSel { get; set; }


        /// <summary>
        /// Цена покупки RUB
        /// </summary>
        public double RubBuy { get; set; }

        /// <summary>
        /// Цена продажи RUB
        /// </summary>
        public double RubSel { get; set; }

    }
}
