using System.IO;
using System.Linq;
using System.Windows;
using Economy.Common.FileSystem;
using Economy.Models;
using System.Collections.Generic;


namespace Economy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DirPathOut = @"..\..\..\Economy\Data\Converted\";
        private IEnumerable<MontlyReport> _reports;
        private readonly List<AccountReport> _reportsByAccaunt = new List<AccountReport>();

        public MainWindow()
        {
            InitializeData();
            InitializeComponent();
            Acconts.ItemsSource = _reportsByAccaunt;
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
            var groups = _reports.OrderBy(i => i.LastDate).GroupBy(i => i.AccountNumber);
            foreach (var itemList in groups)
            {
                var buf = itemList.First();
                var accountReport = new AccountReport
                {
                    AccountNumber = buf.AccountNumber,
                    BancInfo = buf.BancInfo
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
            var groups = _reports.OrderBy(i => i.LastDate).GroupBy(i => i.AccountNumber);
            foreach (var itemList in groups)
            {
                var errors = new List<string>();
                decimal balans = itemList.First().PrevBalance;
                foreach (var item in itemList)
                {
                    if (balans != item.PrevBalance)
                    {
                        errors.Add(
                            string.Format(
                                "Дебит с кредитом не сошлись: Дата {0}. Сумма по месяцам {1}. Сумма по отчету {2}",
                                item.LastDate, balans, item.PrevBalance));
                    }
                    balans += item.TransactionItems.Sum(i => i.AmountByAccount);
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
            var data = ((FrameworkElement) sender).DataContext as AccountReport;
            if (data != null)
            {
                Message.Text = string.Join(System.Environment.NewLine, data.ErrorsList);
                MessageGroupBox.Visibility = Visibility.Visible;
            }
        }

        private void CloseMessageBox(object sender, RoutedEventArgs e)
        {
            MessageGroupBox.Visibility = Visibility.Hidden;
        }
    }
}
