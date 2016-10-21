using System;
using System.Collections.Generic;

namespace Economy.Dtos
{
    /// <summary>
    /// Месячный отчет
    /// </summary>
    public partial class MontlyReportDto
    {
        private List<TransactionDto> _transactionDtos = new List<TransactionDto>();
        private WalletDto _walletDto = new WalletDto();
        

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