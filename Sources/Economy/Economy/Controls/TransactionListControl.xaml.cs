using System;
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
                BalanceNow.Content = ViewModelLocator.TotalBalance;
            }
        }
        
        void SelectedTransactions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            decimal balance = 0;
            var source = ViewModelLocator.SelectedTransactions
                .OrderBy(g => g.TransactionDate)
                .GroupBy(g => string.Format("{0:yyyy MM}", g.TransactionDate))
                .Select(g => new TransactionItemShortViewModel
                {
                    Data = g.Key,
                    Income = g.Where(i => i.IsIncome).Sum(i => ViewModelLocator.GetCashInMainCurrency(i)),
                    Outcome = g.Where(i => !i.IsIncome).Sum(i => ViewModelLocator.GetCashInMainCurrency(i)),
                    IncomeWithoutLocal = g.Where(i => i.IsIncome && !i.IsLocalTransaction).Sum(i => ViewModelLocator.GetCashInMainCurrency(i)),
                    OutcomeWithoutLocal = g.Where(i => !i.IsIncome && !i.IsLocalTransaction).Sum(i => ViewModelLocator.GetCashInMainCurrency(i)),
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
