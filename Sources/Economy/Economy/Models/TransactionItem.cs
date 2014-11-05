using System;

namespace Economy.Models
{
    [Serializable]
    public class TransactionItem
    {
        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Дата транзакции
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Номер транзакции/ документа
        /// </summary>
        public string TransactionCode { get; set; }

        /// <summary>
        /// Код транзакции Описание операции Другая сторона, участвующая в операции
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Валюта транзакции
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Сумма в валюте транзакции
        /// </summary>
        public decimal AmountByCurrency { get; set; }

        /// <summary>
        /// Сумма в валюте счета
        /// </summary>
        public decimal AmountByAccount { get; set; }
    }
}
