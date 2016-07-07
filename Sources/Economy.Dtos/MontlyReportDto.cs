using System;
using System.Collections.Generic;

namespace Economy.Dtos
{
    /// <summary>
    /// Месячный отчет
    /// </summary>
    public class MontlyReportDto : BaseDto
    {
        private List<TransactionDto> _transactionDtos = new List<TransactionDto>();
        private WalletDto _walletDto = new WalletDto();

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// первоначальный баланс счета
        /// </summary>
        public decimal StartBalance { get; set; }

        /// <summary>
        /// итоговое состояние счета
        /// </summary>
        public decimal EndBalance { get; set; }

        /// <summary>
        /// Период действия (год месяц)
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Кошелек
        /// </summary>
        public WalletDto WalletDto
        {
            get { return _walletDto; }
            set { _walletDto = value; }
        }

        /// <summary>
        /// Список транзакций за период
        /// </summary>
        public List<TransactionDto> TransactionDtos
        {
            get { return _transactionDtos; }
            set { _transactionDtos = value; }
        }

        public bool IsNew
        {
            get { return Id == Guid.Empty || WalletDto.IsNew; }
        }
    }
}