using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Economy.Models;
using CommonLibs.Serialization;
using CQRS.Logic;
using Economy.Common;
using Economy.Dtos;
using Economy.IoC;
using Economy.Logic.Queries;
using Economy.Parsers;
using Ninject;

namespace Economy.ViewModels
{
    public class ViewModelLocator : ViewModelBase
    {
        public const string DataDir = @"..\..\..\Economy\Data\";
        public const string BelinvestDirPath = DataDir + @"Sources\Mails\Belinvest";
        public const string PriorDirPath = DataDir + @"Sources\Mails\Prior";
        public const string HandsDirPath = DataDir + @"Sources\Hands\";
        public const string DirHistoryPath = DataDir + @"Sources\History\";
        public const string BelinvestDirPathOut = DataDir + @"Converted\Mails\Belinvest";
        public const string PriorDirPathOut = DataDir + @"Converted\Mails\Prior";
        public const string HandsDirPathOut = DataDir + @"Converted\Hands";
        public const string DirHistoryPathOut = DataDir + @"Converted\History\";


        public RelayCommand UpdateDataFilesCommand { get; private set; }

        public static decimal TotalBalance
        {
            get
            {
                if (History == null)
                    return 0;

                decimal result = 0;
                foreach (var account in Accounts)
                {
                    if (History.MainCurrency.Equals(account.CurrencyType))
                    {
                        result += account.Balance;
                    }
                    else
                    {
                        var historyPrice = History.PriceHistories
                            .OrderByDescending(i => i.RegDate)
                            .FirstOrDefault(i => i.CurrencyType.Equals(account.CurrencyType));
                        result += account.Balance * historyPrice.Buy;
                    }
                }
                return result;
            }
        }

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

        /// <summary>
        /// Ручтое добавление переводов
        /// </summary>
        public static AddCashViewModel AddCash { get; set; }


        public ViewModelLocator()
            : this(false)
        {

        }

        public ViewModelLocator(bool isDebug)
            : base(isDebug)
        {
            UpdateDataFilesCommand = new RelayCommand(UpdateDataFiles);

            Accounts = new ExtendedObservableCollection<AccountViewModel>();
            SelectedTransactions = new ExtendedObservableCollection<TransactionItemViewModel>();
            Information = new InformationViewModel();
            State = new StateViewModel();
            AddCash = new AddCashViewModel(Accounts);


            if (IsDesignTime)
            {
                Accounts.Add(new AccountViewModel());
                SelectedTransactions.Add(new TransactionItemViewModel());
            }
            else
            {
                UpdateDataFilesCommand.Execute(null);
            }
        }


        private void LoadData()
        {
            History = LoadHistory();

            SelectedTransactions.Clear();
            var montlyReport = LoadMontlyReports();

            var accountViewModels = ConvertMontlyReportToAccountReport(montlyReport);
            ValidateReports(accountViewModels, montlyReport);
            SetAmountByByr(accountViewModels);
            FindLocalTransactions(accountViewModels);
            Accounts.Clear();
            Accounts.AddRange(accountViewModels);
        }

        private static IEnumerable<MontlyReportDto> LoadMontlyReports()
        {
            var bank = new BankDto { Name = CourseArhiveConverter.BankName };
            var bml = App.CommandQueryDispatcher.ExecuteQuery<List<MontlyReportDto>>(new MontlyReportGetAllQuery(bank)).Data;

            return bml;
            //var belinvestConvertedPaths = Directory.GetFiles(BelinvestDirPathOut);
            //var bml = belinvestConvertedPaths.Select(XmlSerialization.Deserialize<MontlyReport>);
            //var priorConvertedPaths = Directory.GetFiles(PriorDirPathOut);
            //var pcp = priorConvertedPaths.Select(XmlSerialization.Deserialize<MontlyReport>);
            //var handsConvertedPaths = Directory.GetFiles(HandsDirPathOut);
            //var hcp = handsConvertedPaths.Select(XmlSerialization.Deserialize<MontlyReport>);
            //return bml.Union(pcp).Union(hcp);
        }

        private History LoadHistory()
        {
            var bank = new BankDto { Name = CourseArhiveConverter.BankName };

            var result = App.CommandQueryDispatcher.ExecuteQuery<List<CourseArhiveDto>>(new CourseArhiveGetAllQuery(bank));

            return new History(result.Data, new CurrencyTypeDto());
        }

        private static List<AccountViewModel> ConvertMontlyReportToAccountReport(IEnumerable<MontlyReportDto> montlyReport)
        {
            var accounts = new List<AccountViewModel>();
            var groups = montlyReport
                .OrderBy(i => i.StartDate)
                .GroupBy(i => i.WalletId);

            foreach (var itemList in groups)
            {
                var buf = itemList.First();
                var accountReport = new AccountViewModel
                {
                    WalletId = buf.Wallet.Name,
                    BancInfo = buf.Wallet.Bank.Name,
                    StartBalance = buf.Wallet.StartBalance ?? 0,
                    CurrencyType = buf.Wallet.CurrencyType
                };

                foreach (var item in itemList)
                {
                    item.TransactionDtos.Sort();
                    accountReport.SetTransactionItemRangeSilent(item.TransactionDtos);
                }
                accounts.Add(accountReport);
            }
            return accounts;
        }

        private static void ValidateReports(List<AccountViewModel> accountViewModels, IEnumerable<MontlyReportDto> montlyReport)
        {
            var groups = montlyReport.OrderBy(i => i.StartDate).GroupBy(i => i.WalletId);
            foreach (var itemList in groups)
            {
                var errors = new List<string>();
                var balans = itemList.First().StartBalance;
                //DateTime? startDate = null;
                //DateTime? endDate = null;
                //DateTime? createdDateTime = null;
                foreach (var item in itemList)
                {
                    if (balans != item.StartBalance)
                    {
                        errors.Add(
                            string.Format(
                                "Дата {0} [{1} - {2}]. Сумма по месяцам {3}.{4}Дата {5} [{6} - {7}]. Сумма по отчету {8}. {4}{9}",
                                createdDateTime, startDate, endDate, balans, Environment.NewLine,
                                item.CreatedDateTime, item.StartDate, item.EndDate, item.PrevBalance, "-----------"));
                    }
                    //startDate = item.StartDate;
                    //endDate = item.EndDate;
                    //createdDateTime = item.CreatedDateTime;
                    balans = item.StartBalance + item.Transactions.Sum(i => i.QuantityByWallet.Value);
                }
                var accountViewModel = accountViewModels.SingleOrDefault(i => i.WalletId == itemList.Key);
                if (accountViewModel == null)
                {
                    throw new NullReferenceException("Непредвиденное исключение! Не найдено соответствие в данных.");
                }
                accountViewModel.ErrorsList = errors;
            }
        }

        public async void UpdateDataFiles(object data)
        {
            State.CreateUpdateAction(StateViewModel.Actions.MailConvert, "Выполняется обновление данных.");
            TryConvert(ConvertersModule.HistoryConverter, DirHistoryPath, DirHistoryPathOut, UpdateDataFilesState);
            //TryConvert(ConvertersModule.PriorConverter, PriorDirPath, PriorDirPathOut, UpdateDataFilesState);
            TryConvert(ConvertersModule.BelinvestConverter, BelinvestDirPath, BelinvestDirPathOut, UpdateDataFilesState);

            LoadData();
        }

        private void TryConvert(string parserKey, string dirPath, string dirPathOut, Action<string, int, int> callback)
        {
            try
            {
                var parser = App.Kernel.Get<IConverter>(parserKey);
                if (!Directory.Exists(dirPath))
                    throw new DirectoryNotFoundException(dirPath);
                if (!Directory.Exists(dirPathOut))
                    throw new DirectoryNotFoundException(dirPathOut);

                var paths = Directory.GetFiles(dirPath);
                var convertedPaths = Directory.GetFiles(dirPathOut);
                var index = 0;

                foreach (var filePath in paths)
                {
                    var ext = Path.GetExtension(filePath);
                    if (ext == ".txt" || ext == ".htm")
                    {
                        index++;
                        var fpwe = Path.GetFileNameWithoutExtension(filePath) ?? string.Empty;
                        //if (!convertedPaths.Any(f => fpwe.Equals(Path.GetFileNameWithoutExtension(f), StringComparison.CurrentCultureIgnoreCase)) ||
                        //    parserKey == ConvertersModule.HistoryConverter)
                        //{
                        var outpath = Path.Combine(dirPathOut, Path.GetFileNameWithoutExtension(filePath) + ".xml");
                        parser.ConvertAndSave(filePath, outpath);
                        //}
                        if (callback != null)
                            callback(fpwe, paths.Length, index);
                    }
                }
            }
            catch (Exception exception)
            {
                State.CreateUpdateAction(StateViewModel.Actions.MailConvert, $"Ошибка обновления данных {exception.Message}");
            }
        }

        private static void UpdateDataFilesState(string fileName, int fileCount, int index)
        {
            State.CreateUpdateAction(StateViewModel.Actions.MailConvert, $"Обработка почты: {fileCount} из {index} выполнено");
        }

        private static void SetAmountByByr(IEnumerable<AccountViewModel> accountViewModels)
        {
            foreach (var acc in accountViewModels)
            {
                foreach (var tran in acc.TransactionItems)
                {
                    tran.AmountByMainCurrency = GetCashInMainCurrency(tran);
                }
            }
        }

        private static decimal GetCashInMainCurrency(TransactionItemViewModel model)
        {
            if (model.QuantityByCurrency == 0 && model.QuantityByAccount == 0)
                return 0;

            if (History.MainCurrency.Equals(model.Account.CurrencyType))
            {
                if (model.QuantityByAccount != 0)
                    return model.QuantityByAccount.Value;
            }
            if (History.MainCurrency.Equals(model.CurrencyType))
            {
                return model.QuantityByCurrency;
            }
            var currencyType = model.CurrencyType == null ? model.Account.CurrencyType : model.CurrencyType;
            var historyPrice = History.GetNearest(model.TransactionDate.Value.Date, currencyType.Id);
            if (historyPrice != null)
                return model.QuantityByAccount.Value * historyPrice.Buy;

            throw new ArithmeticException("Недостаточно данных для формирования отчета.");
        }

        private static void FindLocalTransactions(IEnumerable<AccountViewModel> accounts)
        {
            var transactions = new List<TransactionItemViewModel>();
            foreach (var account in accounts)
            {
                transactions.AddRange(account.TransactionItems);
            }

            foreach (var group in transactions.Where(i => i.QuantityByCurrency != 0).GroupBy(t => t.TransactionDate.Value.Date))
            {
                if (group.Count() == 1)
                    continue;
                var buf = group.ToArray();
                for (var i = 0; i < buf.Length - 1; i++)
                {
                    for (var j = i + 1; j < buf.Length; j++)
                    {
                        if (!buf[i].IsLocalTransaction && !buf[j].IsLocalTransaction && buf[i].Account != buf[j].Account
                            && ((buf[i].QuantityByCurrency == -buf[j].QuantityByCurrency && buf[i].CurrencyType == buf[j].CurrencyType)
                            || (buf[i].QuantityByCurrency == -buf[j].QuantityByAccount && buf[i].CurrencyType == buf[j].Account.CurrencyType)
                            || (buf[i].QuantityByAccount == -buf[j].QuantityByCurrency && buf[i].Account.CurrencyType == buf[j].CurrencyType)))
                        {
                            buf[i].IsLocalTransaction = true;
                            buf[j].IsLocalTransaction = true;
                        }
                    }
                }
            }
        }
    }
}