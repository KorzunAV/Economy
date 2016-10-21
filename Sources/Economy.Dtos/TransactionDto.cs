
namespace Economy.Dtos
{
    public partial class TransactionDto
    {
        /// <summary>
        /// Валюта транзакции
        /// </summary>
        public CurrencyTypeDto CurrencyType { get; set; }
        
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