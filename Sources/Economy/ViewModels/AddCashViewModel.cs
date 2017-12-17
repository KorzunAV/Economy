using System.IO;
using System.Linq;
using System.Windows;
using CommonLibs.Serialization;
using Economy.Common;
using Economy.Dtos;
using Economy.Models;

namespace Economy.ViewModels
{
    /// <summary>
    /// Сообщения об ошибках и другой информационный текст
    /// </summary>
    public class AddCashViewModel : ViewModelBase
    {
        private Visibility _addCashGroupBoxVisibility;
        private AccountViewModel _selectedAccount;
        private TransactionItemViewModel _transactionItem;
        private readonly ExtendedObservableCollection<AccountViewModel> _accounts;

        public ExtendedObservableCollection<AccountViewModel> Accounts
        {
            get { return _accounts; }
        }

        public Visibility AddCashGroupBoxVisibility
        {
            get { return _addCashGroupBoxVisibility; }
            set { SetProperty(ref _addCashGroupBoxVisibility, value); }
        }

        public AccountViewModel SelectedAccount
        {
            get { return _selectedAccount ?? (_selectedAccount = Accounts.Any() ? Accounts.First() : null); }
            set { _selectedAccount = value; }
        }

        public TransactionItemViewModel TransactionItem
        {
            get
            {
                return _transactionItem ?? (_transactionItem = new TransactionItemViewModel());
            }
            set { _transactionItem = value; }
        }


        public RelayCommand ShowCommand { get; private set; }

        public RelayCommand HideCommand { get; set; }

        public RelayCommand SaveCommand { get; set; }


        public AddCashViewModel(ExtendedObservableCollection<AccountViewModel> accounts)
        {
            if (IsDesignTime)
            {
                AddCashGroupBoxVisibility = Visibility.Visible;
            }
            else
            {
                AddCashGroupBoxVisibility = Visibility.Hidden;
            }

            HideCommand = new RelayCommand(Hide);
            ShowCommand = new RelayCommand(Show);
            SaveCommand = new RelayCommand(Save);
            _accounts = accounts;
        }

        private void Hide(object param)
        {
            AddCashGroupBoxVisibility = Visibility.Hidden;
        }

        private void Show(object param)
        {
            AddCashGroupBoxVisibility = Visibility.Visible;
        }

        private void Save(object param)
        {
            TransactionItem.Account = SelectedAccount;
            var transactionItemViewModel = TransactionItem.Clone();
            SelectedAccount.SetTransactionItem(transactionItemViewModel);

            var convertedPaths = Directory.GetFiles(ViewModelLocator.PriorDirPathOut)
                .Union(Directory.GetFiles(ViewModelLocator.BelinvestDirPathOut));
            
            foreach (var path in convertedPaths )
            {
                var montlyReport = XmlSerialization.Deserialize<MontlyReportDto>(path);
                if (montlyReport.WalletId == SelectedAccount.WalletId 
                    && montlyReport.StartDate >= TransactionItem.TransactionDate 
                    && montlyReport.EndDate <= TransactionItem.TransactionDate)
                {
                    var transactionItem = AutoMapper.Mapper.Map<TransactionDto>(TransactionItem);
                    montlyReport.TransactionDtos.Add(transactionItem);
                    XmlSerialization.Serialize(montlyReport, path, FileMode.Open);
                    break;
                }
            }
        }
    }
}