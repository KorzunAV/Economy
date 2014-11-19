using System;
using Economy.Models;

namespace Economy.ViewModels
{
    public class TransactionItemViewModel : ViewModelBase
    {
        #region Поля

        private DateTime _registrationDate;
        private DateTime _transactionDate;
        private string _transactionCode;
        private string _description;
        private string _currency;
        private decimal _amountByCurrency;
        private decimal _amountByAccount;
        private string _accountNumber;

        #endregion Поля

        #region Свойства

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDate
        {
            get { return _registrationDate; }
            set { SetProperty(ref _registrationDate, value); }
        }

        /// <summary>
        /// Дата транзакции
        /// </summary>
        public DateTime TransactionDate
        {
            get { return _transactionDate; }
            set { SetProperty(ref _transactionDate, value); }
        }

        /// <summary>
        /// Номер транзакции/ документа
        /// </summary>
        public string TransactionCode
        {
            get { return _transactionCode; }
            set { SetProperty(ref _transactionCode, value); }
        }

        /// <summary>
        /// Код транзакции Описание операции Другая сторона, участвующая в операции
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        /// <summary>
        /// Валюта транзакции
        /// </summary>
        public string Currency
        {
            get { return _currency; }
            set { SetProperty(ref _currency, value); _currency = value; }
        }

        /// <summary>
        /// Сумма в валюте транзакции
        /// </summary>
        public decimal AmountByCurrency
        {
            get { return _amountByCurrency; }
            set { SetProperty(ref _amountByCurrency, value); }
        }

        /// <summary>
        /// Сумма в валюте счета
        /// </summary>
        public decimal AmountByAccount
        {
            get { return _amountByAccount; }
            set { SetProperty(ref _amountByAccount, value); }
        }

        /// <summary>
        /// Номер счета
        /// </summary>
        public string AccountNumber
        {
            get { return _accountNumber; }
            set { SetProperty(ref _accountNumber, value); }
        }

        #endregion Свойства

        public TransactionItemViewModel()
        {
            if (IsDesignTime)
            {
                _registrationDate = DateTime.Today;
                _transactionDate = DateTime.Today;
                _transactionCode = "TransactionCode";
                _description = "Description";
                _currency = "Currency";
                _amountByCurrency = 42;
                _amountByAccount = 42;
                _accountNumber = "AccountNumber";
            }
        }

        public TransactionItemViewModel(TransactionItem item, string accountNumber)
        {
            SetSilent(item, accountNumber);
        }

        public void SetSilent(TransactionItem item, string accountNumber)
        {
            _registrationDate = item.RegistrationDate;
            _transactionDate = item.TransactionDate;
            _transactionCode = item.TransactionCode;
            _description = item.Description;
            _currency = item.Currency;
            _amountByCurrency = item.AmountByCurrency;
            _amountByAccount = item.AmountByAccount;
            _accountNumber = accountNumber;
        }


        public int CompareTo(TransactionItem other)
        {
            return RegistrationDate.CompareTo(other.RegistrationDate);
        }
    }
}
