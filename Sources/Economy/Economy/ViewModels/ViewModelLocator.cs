﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Economy.Common.FileSystem;
using Economy.Models;

namespace Economy.ViewModels
{
    public class ViewModelLocator : ViewModelBase
    {
        const string DirPathOut = @"..\..\..\Economy\Data\Converted\";

        public static ObservableCollection<AccountViewModel> Accounts { get; set; }

        public static ObservableCollection<TransactionItemViewModel> SelectedTransactions { get; set; }

        public static InformationViewModel Information { get; set; }
     
        public ViewModelLocator()
        {
            Accounts = new ObservableCollection<AccountViewModel>();
            SelectedTransactions = new ObservableCollection<TransactionItemViewModel>();
            Information = new InformationViewModel();

            if (IsDesignTime)
            {
                Accounts.Add(new AccountViewModel());
                SelectedTransactions.Add(new TransactionItemViewModel());
            }
            else
            {
                var montlyReport = LoadMontlyReports();
                var accountViewModels =  ConvertMontlyReportToAccountReport(montlyReport);
                ValidateReports(accountViewModels, montlyReport);
                Accounts.Clear();
                foreach (var accountViewModel in accountViewModels)
                {
                    Accounts.Add(accountViewModel);
                }
            }
        }

        private List<MontlyReport> LoadMontlyReports()
        {
            var convertedPaths = Directory.GetFiles(DirPathOut);
            return convertedPaths.Select(Deserialization.Load<MontlyReport>).ToList();
        }

        private List<AccountViewModel> ConvertMontlyReportToAccountReport(IEnumerable<MontlyReport> montlyReport)
        {
            var accounts = new List<AccountViewModel>();
            var groups = montlyReport.OrderBy(i => i.CreatedDateTime).GroupBy(i => i.AccountNumber);
            foreach (var itemList in groups)
            {
                var buf = itemList.First();
                var accountReport = new AccountViewModel
                {
                    AccountNumber = buf.AccountNumber,
                    BancInfo = buf.BancInfo,
                    StartBalance = buf.PrevBalance,
                    Currency = buf.AccountCurrency
                };

                foreach (var item in itemList)
                {
                    item.TransactionItems.Sort();
                    accountReport.SetTransactionItemRangeSilent(item.TransactionItems);
                }
                accounts.Add(accountReport);
            }
            return accounts;
        }
        
        private void ValidateReports(List<AccountViewModel> accountViewModels, IEnumerable<MontlyReport> montlyReport)
        {
            var groups = montlyReport.OrderBy(i => i.CreatedDateTime).GroupBy(i => i.AccountNumber);
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
                var accountViewModel = accountViewModels.SingleOrDefault(i => i.AccountNumber == itemList.Key);
                if (accountViewModel == null)
                {
                    throw new NullReferenceException("Непредвиденное исключение! Не найдено соответствие в данных.");
                }
                accountViewModel.ErrorsList = errors;
            }
        }
    }
}