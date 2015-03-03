using System;
using System.Collections.Generic;

namespace Economy.Models
{
    [Serializable]
    public class History
    {
        public string MainCurency
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
