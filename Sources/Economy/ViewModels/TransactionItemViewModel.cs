using System;
using System.Windows.Media;
using Economy.Dtos;

namespace Economy.ViewModels
{
    public class TransactionItemViewModel : ViewModelBase
    {
        #region Поля

        private DateTime _registrationDate;
        private DateTime? _transactionDate;
        private string _code;
        private string _description;
        private CurrencyTypeDto _currencyType;
        private decimal _quantityByCurrency;
        private decimal? _quantityByAccount;
        private decimal _amountByMainCurrency;
        private string _accountNumber;
        private AccountViewModel _account;

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
        public DateTime? TransactionDate
        {
            get { return _transactionDate; }
            set { SetProperty(ref _transactionDate, value); }
        }

        /// <summary>
        /// Номер транзакции/ документа
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { SetProperty(ref _code, value); }
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
        public CurrencyTypeDto CurrencyType
        {
            get { return _currencyType; }
            set { SetProperty(ref _currencyType, value); _currencyType = value; }
        }

        /// <summary>
        /// Сумма в валюте транзакции
        /// </summary>
        public decimal QuantityByCurrency
        {
            get { return _quantityByCurrency; }
            set { SetProperty(ref _quantityByCurrency, value); }
        }

        /// <summary>
        /// Сумма в валюте счета
        /// </summary>
        public decimal? QuantityByAccount
        {
            get { return _quantityByAccount; }
            set { SetProperty(ref _quantityByAccount, value); }
        }
        
        /// <summary>
        /// Сумма в валюте счета
        /// </summary>
        public decimal AmountByMainCurrency
        {
            get { return _amountByMainCurrency; }
            set { SetProperty(ref _amountByMainCurrency, value); }
        }


        /// <summary>
        /// Коммиссия за перевод
        /// </summary>
        public decimal? Commission { get; set; }

        /// <summary>
        /// Номер счета
        /// </summary>
        public string AccountNumber
        {
            get { return Account.AccountNumber; }
        }

        /// <summary>
        /// Ссылка на аккаунт
        /// </summary>
        public AccountViewModel Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }

        public bool IsIncome
        {
            get
            {
                return (QuantityByCurrency > 0 || QuantityByAccount > 0) ||
                    (QuantityByCurrency == 0 && QuantityByAccount == 0);
            }
        }

        public Brush DataRowColor
        {
            get
            {
                var cl = IsIncome ? Colors.Green : Colors.Red;
                return new SolidColorBrush(cl);
            }
        }

        /// <summary>
        /// Является ли транцакция внутри счетов (по своим карточкам)
        /// </summary>
        public bool IsLocalTransaction { get; set; }

        /// <summary>
        /// Является ли обьект новым (требует сохранения на диск)
        /// </summary>
        public bool IsNew { get; set; }

        #endregion Свойства

        public TransactionItemViewModel()
        {
            if (IsDesignTime)
            {
                _registrationDate = DateTime.Today;
                _transactionDate = DateTime.Today;
                _code = "Code";
                _description = "Description";
                _currencyType = new CurrencyTypeDto {Name = "Currency" , ShortName = "Currency" };
                _quantityByCurrency = 42;
                _quantityByAccount = 42;
                _accountNumber = "AccountNumber";
            }
            else
            {
                IsNew = true;
            }
        }

        public TransactionItemViewModel(TransactionDto dto, AccountViewModel account)
        {
            SetSilent(dto, account);
        }

        public void SetSilent(TransactionDto dto, AccountViewModel account)
        {
            _registrationDate = dto.RegistrationDate;
            _transactionDate = dto.TransactionDate;
            _code = dto.Code;
            _description = dto.Description;
            _currencyType = dto.CurrencyType;
            _quantityByCurrency = dto.QuantityByCurrency;
            _quantityByAccount = dto.QuantityByAccount;
            Account = account;
        }

        public TransactionItemViewModel Clone()
        {
            return new TransactionItemViewModel()
            {
                RegistrationDate = RegistrationDate,
                TransactionDate = TransactionDate,
                Code = Code,
                Description = Description,
                CurrencyType = CurrencyType,
                QuantityByCurrency = QuantityByCurrency,
                QuantityByAccount = QuantityByAccount,
                Account = Account
            };
        }

        public int CompareTo(TransactionDto other)
        {
            return RegistrationDate.CompareTo(other.RegistrationDate);
        }
    }
}
