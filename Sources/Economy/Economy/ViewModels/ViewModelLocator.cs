using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Economy.Common.FileSystem;
using Economy.Models;
using Economy.Data;

namespace Economy.ViewModels
{
    public class ViewModelLocator : ViewModelBase
    {
        const string DataDir = @"..\..\..\Economy\Data\";
        const string DirPath = DataDir + @"Sources\Mails\";
        const string DirHistoryPath = DataDir + @"Sources\History\";
        const string DirPathOut = DataDir + @"Converted\Mails\";
        const string DirHistoryPathOut = DataDir + @"Converted\History\";

        public static ExtendedObservableCollection<AccountViewModel> Accounts { get; set; }

        public static ExtendedObservableCollection<TransactionItemViewModel> SelectedTransactions { get; set; }

        /// <summary>
        /// Сообщения об ошибках и другой информационный текст
        /// </summary>
        public static InformationViewModel Information { get; set; }

        /// <summary>
        /// Пользовательские настройки
        /// </summary>
        public static SettingsViewModel Settings { get; set; }

        /// <summary>
        /// История курсов валют
        /// </summary>
        public static History History { get; set; }
        
        /// <summary>
        /// Статус процесса
        /// </summary>
        public static StateViewModel State { get; set; }

        public ViewModelLocator()
            : this(false)
        {

        }

        public ViewModelLocator(bool isDebug)
            : base(isDebug)
        {
            Accounts = new ExtendedObservableCollection<AccountViewModel>();
            SelectedTransactions = new ExtendedObservableCollection<TransactionItemViewModel>();
            Information = new InformationViewModel();
            State = new StateViewModel();

            if (IsDesignTime)
            {
                Accounts.Add(new AccountViewModel());
                SelectedTransactions.Add(new TransactionItemViewModel());
            }
            else
            {
                LoadData();
            }
        }


        private static void LoadData()
        {
            SelectedTransactions.Clear();
            var montlyReport = LoadMontlyReports();
            History = LoadHistory();
            var accountViewModels = ConvertMontlyReportToAccountReport(montlyReport);
            ValidateReports(accountViewModels, montlyReport);
            Accounts.Clear();
            Accounts.AddRange(accountViewModels);
        }

        private static List<MontlyReport> LoadMontlyReports()
        {
            var convertedPaths = Directory.GetFiles(DirPathOut);
            return convertedPaths.Select(Deserialization.Load<MontlyReport>).ToList();
        }

        private static History LoadHistory()
        {
            if (!File.Exists(DirHistoryPathOut + "Courses.xml"))
                return null;
            return Deserialization.Load<History>(DirHistoryPathOut + "Courses.xml");
        }

        private static List<AccountViewModel> ConvertMontlyReportToAccountReport(IEnumerable<MontlyReport> montlyReport)
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

        private static void ValidateReports(List<AccountViewModel> accountViewModels, IEnumerable<MontlyReport> montlyReport)
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

        public async static void UpdateDataFiles()
        {
            try
            {
                State.CreateUpdateAction(StateViewModel.Actions.MailConvert, "Выполняется обновление данных.");
                var manager = new ConvertManager();
                await manager.Convert(DirPath, DirPathOut, UpdateDataFilesState);
                await manager.Convert(DirHistoryPath, DirHistoryPathOut, UpdateDataFilesState);
                LoadData();
            }
            catch (Exception exception)
            {
                State.CreateUpdateAction(StateViewModel.Actions.MailConvert, string.Format("Ошибка обновления данных {0}", exception.Message));
            }
        }

        private static void UpdateDataFilesState(string fileName, int fileCount, int index)
        {
            State.CreateUpdateAction(StateViewModel.Actions.MailConvert, string.Format("Обработка почты: {0} из {1} выполнено", fileCount, index));
        }
    }
}
