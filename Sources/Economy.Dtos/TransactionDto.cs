using System;

namespace Economy.Dtos
{
    public class TransactionDto : BaseDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Дата транзакции
        /// </summary>
        public DateTime? TransactionDate { get; set; }

        /// <summary>
        /// Номер транзакции/ документа
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Код транзакции Описание операции Другая сторона, участвующая в операции
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Валюта транзакции
        /// </summary>
        public CurrencyTypeDto CurrencyType { get; set; }

        /// <summary>
        /// Сумма в валюте транзакции
        /// </summary>
        public decimal QuantityByCurrency { get; set; }

        /// <summary>
        /// Сумма в валюте счета
        /// </summary>
        public decimal? QuantityByAccount { get; set; }

        /// <summary>
        /// Коммиссия за перевод
        /// </summary>
        public decimal? Commission { get; set; }

        /// <summary>
        /// перевод с счета
        /// </summary>
        public Guid? FromWalletId { get; set; }

        /// <summary>
        /// перевод на счет
        /// </summary>
        public Guid? ToWalletId { get; set; }

        /// <summary>
        /// Отчетный период
        /// </summary>
        public MontlyReportDto MontlyReport { get; set; }

        public int CompareTo(TransactionDto other)
        {
            return RegistrationDate.CompareTo(other.RegistrationDate);
        }
    }
}