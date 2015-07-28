using System;
using System.Collections.Generic;

namespace Economy.Models
{
    [Serializable]
    public class History
    {
        public string MainCurrency
        {
            get
            {
                return "BYR";
            }
        }

        public List<PriceHistory> PriceHistories
        {
            get;
            set;
        }
    }
}
