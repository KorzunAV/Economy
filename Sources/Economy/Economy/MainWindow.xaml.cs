using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Economy.Common.FileSystem;
using Economy.Models;
using System.Collections.Generic;


namespace Economy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        const string DirPathOut = @"..\..\..\Economy\Data\Converted\";
        private IEnumerable<MontlyReport> _reports;
        private readonly List<AccountReport> _reportsByAccaunt = new List<AccountReport>();

        public MainWindow()
        {
            InitializeData();
            InitializeComponent();
            Acconts.List.ItemsSource = _reportsByAccaunt;
            Acconts.ShowErrorEvent += ShowErrorList;
            Acconts.List.SelectionChanged += AccontsSelectionChanged;
            TransactionList.List.ItemsSource = _reportsByAccaunt.First().TransactionItems;
        }

        void AccontsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var typedSender = sender as ListBox;
            TransactionList.List.ItemsSource = ((AccountReport)typedSender.SelectedItem).TransactionItems;
           
        }


        private void InitializeData()
        {
            LoadReports();
            MergeData();
            ValidateReports();
            SortData();
        }

        private void MergeData()
        {
            var groups = _reports.OrderBy(i => i.CreatedDateTime).GroupBy(i => i.AccountNumber);
            foreach (var itemList in groups)
            {
                var buf = itemList.First();
                var accountReport = new AccountReport
                {
                    AccountNumber = buf.AccountNumber,
                    BancInfo = buf.BancInfo,
                    StartBalance = buf.PrevBalance,
                    Currency = buf.AccountCurrency
                };

                foreach (var item in itemList)
                {
                    accountReport.TransactionItems.AddRange(item.TransactionItems);
                }
                _reportsByAccaunt.Add(accountReport);
            }
        }

        private void ValidateReports()
        {
            var groups = _reports.OrderBy(i => i.CreatedDateTime).GroupBy(i => i.AccountNumber);
            foreach (var itemList in groups)
            {
                var errors = new List<string>();
                decimal balans = itemList.First().PrevBalance;
                DateTime? startDate = null;
                DateTime? endDate = null;
                DateTime? createdDateTime = null;
                foreach (var item in itemList)
                {
                    if (balans != item.PrevBalance)
                    {
                        errors.Add(
                            string.Format(
                                "Дата {0} [{1} - {2}]. Сумма по месяцам {3}.{4}Дата {5} [{6} - {7}]. Сумма по отчету {8}. {4}{9}",
                                createdDateTime, startDate, endDate, balans, Environment.NewLine,
                                item.CreatedDateTime, item.StartDate, item.EndDate, item.PrevBalance, "-----------"));
                    }
                    startDate = item.StartDate;
                    endDate = item.EndDate;
                    createdDateTime = item.CreatedDateTime;
                    balans = item.PrevBalance + item.TransactionItems.Sum(i => i.AmountByAccount);
                }
                _reportsByAccaunt.SingleOrDefault(i => i.AccountNumber == itemList.Key).ErrorsList = errors;
            }
        }

        private void LoadReports()
        {
            var convertedPaths = Directory.GetFiles(DirPathOut);
            _reports = convertedPaths.Select(Deserialization.Load<MontlyReport>);
        }

        private void SortData()
        {
            foreach (var item in _reportsByAccaunt)
            {
                item.TransactionItems.Sort();
            }
        }

        private void ShowErrorList(object sender, RoutedEventArgs e)
        {
            var data = ((FrameworkElement)sender).DataContext as AccountReport;
            MessageGroupBox.Visibility = Visibility.Visible;
            UpdateErrorList(data);
        }
        
        private void UpdateErrorList(AccountReport accountReport)
        {
            if (accountReport != null && MessageGroupBox.Visibility == Visibility.Visible)
            {
                MessageGroupBox.MessageTextBlock.Text = string.Join(Environment.NewLine, accountReport.ErrorsList);
            }
        }
    }
}
