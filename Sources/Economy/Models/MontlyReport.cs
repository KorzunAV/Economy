using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Economy.Dtos;

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
        public CurrencyTypeDto AccountCurrency { get; set; }

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

        /// <summary>
        /// Переводы
        /// </summary>
        public List<TransactionDto> TransactionDtos { get; set; }


        public DateTime? StartDate
        {
            get
            {
                return GetDate(PeriodInfo, false);
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return GetDate(PeriodInfo, true);
            }
        }

        public DateTime? CreatedDateTime
        {
            get
            {
                return GetDate(CreationInfo ?? PeriodInfo);
                //var date = CreationInfo.Substring(CreationInfo.IndexOf("-", StringComparison.Ordinal) - 2, 19);
                //return DateTime.Parse(date);
            }
        }


        private static DateTime GetDate(string line, bool fromEnd = false)
        {
            try
            {
                var m = Regex.Matches(line, "(0[1-9]|[1-2][0-9]|3[0-1])(-|.)(0[1-9]|1[0-2])(-|.)[0-9]{4}( *)([0-2][0-9]:[0-5][0-9]:[0-5][0-9])?");
                
                var date = fromEnd
                    ? m[1].Value
                    : m[0].Value;

                return DateTime.Parse(date);
            }
            catch 
            {
                throw;
            }
        }
    }
}
