using System;
using System.Collections.Generic;
using System.Linq;

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

        public DateTime? LastDate {
            get { return TransactionItems.Any() ? TransactionItems.Last().RegistrationDate : (DateTime?)null; }
        }

        /// <summary>
        /// Переводы
        /// </summary>
        public List<TransactionItem> TransactionItems { get; set; }

    }
}
