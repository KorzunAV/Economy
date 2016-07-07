using System;
using BLToolkit.Mapping;
using Economy.Dtos.Shared;

namespace Economy.DataAccess.Entities
{
    internal class TransactionEntity : BaseEntity
    {
        /// <summary>
        /// Дата регистрации
        /// </summary>
        [MapField("registration_date")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Дата транзакции
        /// </summary>
        [MapField("transaction_date")]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Номер транзакции/ документа
        /// </summary>
        [MapField("code")]
        public string TransactionCode { get; set; }

        /// <summary>
        /// Код транзакции Описание операции Другая сторона, участвующая в операции
        /// </summary>
        [MapField("description")]
        public string Description { get; set; }

        /// <summary>
        /// Валюта транзакции
        /// </summary>
        [MapField("currency_type")]
        public int CurrencyTypeId { get; set; }

        /// <summary>
        /// Валюта транзакции
        /// </summary>
        public CurrencyTypeDto CurrencyType { get; set; }

        /// <summary>
        /// Сумма в валюте транзакции
        /// </summary>
        [MapField("quantity_by_currency")]
        public decimal AmountByCurrency { get; set; }

        /// <summary>
        /// Сумма в валюте счета
        /// </summary>
        [MapField("quantity_by_account")]
        public decimal AmountByAccount { get; set; }

        /// <summary>
        /// Коммиссия за перевод
        /// </summary>
        [MapField("commission")]
        public decimal Commission { get; set; }

        /// <summary>
        /// перевод с счета
        /// </summary>
        [MapField("from_wallet")]
        public int FromWalletId { get; set; }

        /// <summary>
        /// перевод на счет
        /// </summary>
        [MapField("to_wallet")]
        public int ToWalletId { get; set; }

        /// <summary>
        /// Коммиссия за перевод
        /// </summary>
        [MapField("montly_report")]
        public int MontlyReportId { get; set; }
    }
}