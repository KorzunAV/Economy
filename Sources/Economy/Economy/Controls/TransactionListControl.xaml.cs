using System;
using System.Linq;
using Economy.ViewModels;
using System.Windows;

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
                BalanceNow.Content = GetBalanceNow();
            }
        }


        string GetBalanceNow()
        {
            decimal result = 0;
            foreach (var account in ViewModelLocator.Accounts)
            {
                if (ViewModelLocator.History.MainCurency.Equals(account.Currency, StringComparison.OrdinalIgnoreCase))
                {
                    result += account.Balance;
                }
                else
                {
                    var historyPrice = ViewModelLocator.History.PriceHistories
                        .OrderByDescending(i => i.Date)
                        .FirstOrDefault(i => i.Currency.Equals(account.Currency));
                    result += account.Balance * historyPrice.Buy;
                }
            }
            return string.Format("{0:### ### ###}", result);
        }

        void SelectedTransactions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            decimal balance = 0;
            var source = ViewModelLocator.SelectedTransactions
                .OrderBy(g => g.TransactionDate)
                .GroupBy(g => string.Format("{0:yyyy MM}", g.TransactionDate))
                .Select(g => new TransactionItemShortViewModel
                {
                    Data = g.Key,
                    Income = g.Where(i => i.IsIncome).Sum(i => GetCurrency(i)),
                    Outcome = g.Where(i => !i.IsIncome).Sum(i => GetCurrency(i))
                }).ToList();

            foreach (var item in source)
            {
                item.Balance = balance + item.InOut;
                balance = item.Balance;
            }
            dgIncomeOutcome.ItemsSource = source;
        }

        private decimal GetCurrency(TransactionItemViewModel model)
        {
            if (ViewModelLocator.History.MainCurency.Equals(model.Account.Currency, StringComparison.OrdinalIgnoreCase))
            {
                if (model.AmountByAccount != 0)
                    return model.AmountByAccount;
            }
            if (ViewModelLocator.History.MainCurency.Equals(model.Currency))
            {
                return model.AmountByCurrency;
            }
            var currency = string.IsNullOrEmpty(model.Currency) ? model.Account.Currency : model.Currency;
            var historyPrice = ViewModelLocator.History.PriceHistories
                .FirstOrDefault(i => i.Date.Date == model.TransactionDate.Date && i.Currency.Equals(currency));
            if (historyPrice != null)
                return model.AmountByAccount * historyPrice.Buy;

            throw new ArithmeticException("Недостаточно данных для формирования отчета.");
        }
    }
}
