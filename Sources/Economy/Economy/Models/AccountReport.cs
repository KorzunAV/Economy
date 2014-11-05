using System.Collections.Generic;

namespace Economy.Models
{
    public class AccountReport
    {
        /// <summary>
        /// Номер счета
        /// </summary>
        public string AccountNumber { get; set; }


        /// <summary>
        /// Информация о банке
        /// </summary>
        public string BancInfo { get; set; }



        /// <summary>
        /// Переводы
        /// </summary>
        public List<TransactionItem> TransactionItems { get; set; }

        /// <summary>
        /// Список ошибок валидации
        /// </summary>
        public List<string> ErrorsList { get; set; }

        /// <summary>
        /// Корректность обработки
        /// </summary>
        public bool IsError
        {
            get
            {
                return (ErrorsList != null && ErrorsList.Count > 0);
            }
        }
        
        public AccountReport()
        {
            TransactionItems = new List<TransactionItem>();
        }
    }
}
