using System;
using System.Collections.Generic;

namespace Economy.Models
{
    [Serializable]
    public class MontlyReport
    {
        #region Title
        /// <summary>
        /// 
        /// </summary>
        public string UserInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BancInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReportInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PeriodInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreationInfo { get; set; }

        #endregion Title

        #region Header
        /// <summary>
        /// Номер счета
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Валюта счета
        /// </summary>
        public string AccountCurrency { get; set; }

        /// <summary>
        /// Неснижаемый остаток
        /// </summary>
        public decimal ImmutableBalance { get; set; }

        /// <summary>
        /// Минимальный баланс
        /// </summary>
        public decimal MinBalance { get; set; }

        /// <summary>
        /// Разрешенный кредит
        /// </summary>
        public decimal AvailibleCredit { get; set; }

        /// <summary>
        /// Начальный остаток
        /// </summary>
        public decimal PrevBalance { get; set; }

        #endregion Header

        public DateTime? StartDate
        {
            get
            {
                var date1 = PeriodInfo.Substring(PeriodInfo.IndexOf("-") - 2, 10);
                return DateTime.Parse(date1);
            }
        }

        public DateTime? EndDate {
            get
            {
                var date2 = PeriodInfo.Substring(PeriodInfo.LastIndexOf("-") - 5, 10);
                return DateTime.Parse(date2);
            }
        }

        public DateTime? CreatedDateTime
        {
            get
            {
                var date = CreationInfo.Substring(CreationInfo.IndexOf("-") - 2,19);
                return DateTime.Parse(date);
            }
        }

        /// <summary>
        /// Переводы
        /// </summary>
        public List<TransactionItem> TransactionItems { get; set; }

    }
}
