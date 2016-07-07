using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Economy.ViewModels;

namespace Economy.Controls
{
    /// <summary>
    /// Interaction logic for TransactionListControl.xaml
    /// </summary>
    public partial class TransactionListControl
    {
        public TransactionListControl()
        {
            InitializeComponent();
            if (!(Application.Current == null || Application.Current.GetType() == typeof(Application)))
            {
                ViewModelLocator.SelectedTransactions.CollectionChanged += SelectedTransactions_CollectionChanged;
                dgAll.ItemsSource = ViewModelLocator.SelectedTransactions;
            }
        }

        void SelectedTransactions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            decimal balance = 0;
            
            BalanceNow.Content = ViewModelLocator.TotalBalance;

            var source = ViewModelLocator.SelectedTransactions
                .OrderBy(g => g.TransactionDate)
                .GroupBy(g => $"{g.TransactionDate:yyyy MM}")
                .Select(g => new TransactionItemShortViewModel
                {
                    Data = g.Key,
                    Income = g.Where(i => i.IsIncome).Sum(i => i.AmountByMainCurrency),
                    Outcome = g.Where(i => !i.IsIncome).Sum(i => i.AmountByMainCurrency),
                    IncomeWithoutLocal = g.Where(i => i.IsIncome && !i.IsLocalTransaction).Sum(i => i.AmountByMainCurrency),
                    OutcomeWithoutLocal = g.Where(i => !i.IsIncome && !i.IsLocalTransaction).Sum(i => i.AmountByMainCurrency),
                }).ToList();

            foreach (var item in source)
            {
                balance += item.InOut;
                item.Balance = balance;
            }
            dgIncomeOutcome.ItemsSource = source;
        }
    }
}
