using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using CQRS.Logic;
using Economy.Dtos;
using Economy.Helpers;
using Economy.Logic.Commands;

namespace Economy.Parsers
{
    public class BelinvestConverter : MailParser, IConverter
    {
        public BelinvestConverter(ICommandQueryDispatcher commandQueryDispatcher)
            : base(commandQueryDispatcher) { }

        public void ConvertAndSave(string filePath, string outFilePath)
        {
            string file;
            using (var sr = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
            {
                file = sr.ReadToEnd();
            }
            file = CleanFile(file);
            var tree = ToDocument(file);
            var montlyReport = new MontlyReportDto();

            var result = ReadPreambula(tree.Root);

            if (result)
                result = ReadTitle(tree.Root, montlyReport);
            if (result)
                result = ReadHeader(tree.Root, montlyReport);
            if (result)
                result = ReadTransactionItems(tree.Root, montlyReport);

            if (!result)
                throw new ArithmeticException("Где то несраслось.");

            var montlyReportDtos = new List<MontlyReportDto>();
            var groups = montlyReport.TransactionDtos.OrderBy(i => i.RegistrationDate).GroupBy(i => i.RegistrationDate.Year * 100 + i.RegistrationDate.Month);
            var startBalance = montlyReport.StartBalance;
            foreach (var group in groups)
            {
                var montlyReportByYearMonth = new MontlyReportDto()
                {
                    StartBalance = startBalance,
                    EndBalance = startBalance,
                    StartDate = new DateTime(group.Key / 100, group.Key % 100, 1),
                    WalletDto = montlyReport.WalletDto
                };

                montlyReportByYearMonth.TransactionDtos = group.ToList();
                montlyReportByYearMonth.EndBalance = montlyReportByYearMonth.TransactionDtos.Sum(i => i.QuantityByWallet.Value);
            }

            CommandQueryDispatcher.ExecuteCommand<bool>(new MontlyReportSaveAllCommand(montlyReportDtos));
        }

        private bool ReadPreambula(XElement root)
        {
            var titleTable = root.Elements().First();
            var elem = titleTable.Elements().ToArray();
            if (elem.Length != 1)
            {
                return false;
            }
            //var title = elem[0].Value;
            titleTable.Remove();
            return true;
        }

        private bool ReadTitle(XElement root, MontlyReportDto montlyReport)
        {
            var titleTable = root.Elements().First();
            var infoLines = titleTable.Elements().ToArray();
            if (infoLines.Length != 5)
                return false;

            montlyReport.WalletDto.User.Name = infoLines[0].Elements().Single().Value;
            //montlyReport.WalletDto.BancInfo = infoLines[1].Elements().Single().Value;
            //montlyReport.ReportInfo = infoLines[2].Elements().Single().Value;
            montlyReport.StartDate = GetDate(infoLines[3].Elements().Single().Value);
            //montlyReport.CreationInfo = infoLines[4].Elements().Single().Value;
            titleTable.Remove();
            return true;
        }

        private static DateTime GetDate(string line, bool fromEnd = false)
        {
            var m = Regex.Matches(line, "(0[1-9]|[1-2][0-9]|3[0-1])(-|.)(0[1-9]|1[0-2])(-|.)[0-9]{4}( *)([0-2][0-9]:[0-5][0-9]:[0-5][0-9])?");

            var date = fromEnd
                ? m[1].Value
                : m[0].Value;

            return DateTime.Parse(date);
        }

        private bool ReadHeader(XElement root, MontlyReportDto montlyReport)
        {
            var hederTable = root.Elements().First();
            var infoLines = hederTable.Elements().ToArray();
            if (infoLines.Length != 4)
                return false;

            //montlyReport.ImmutableBalance = StringConverter.StringToDecimal(infoLines[0].Elements().Skip(3).First().Value);
            montlyReport.WalletDto.Name = infoLines[1].Elements().Skip(1).First().Value;
            //montlyReport.MinBalance = StringConverter.StringToDecimal(infoLines[1].Elements().Skip(3).First().Value);
            var currencyTypeShortName = infoLines[2].Elements().Skip(1).First().Value;
            montlyReport.WalletDto.CurrencyType = CurrencyTypeDtos.FirstOrDefault(i => i.ShortName.Equals(currencyTypeShortName));
            //montlyReport.AvailibleCredit = StringConverter.StringToDecimal(infoLines[2].Elements().Skip(3).First().Value);
            montlyReport.StartBalance = StringConverter.StringToDecimal(infoLines[3].Elements().Skip(3).First().Value);
            hederTable.Remove();
            return true;
        }

        private bool ReadTransactionItems(XElement root, MontlyReportDto montlyReport)
        {
            var items = root.Elements().ToArray();
            decimal sumAll = 0;

            for (var i = 1; i < items.Length - 1; i++)
            {
                var item = ReadTransactionItem(items[i]);
                if (item != null)
                {
                    montlyReport.TransactionDtos.Add(item);
                }
                else
                {
                    var sum = Sum(items[i]);
                    if (sum != null)
                    {
                        sumAll = sum.Value;
                    }
                }
            }

            var cur = montlyReport.TransactionDtos.Sum(itm => itm.QuantityByWallet);
            if (cur + montlyReport.StartBalance != sumAll)
            {
                throw new ArithmeticException("Суммы не сошлись");
            }

            return true;
        }

        private decimal? Sum(XElement element)
        {
            var cols = element.Elements().Single().Elements().ToArray();
            if (cols.Length == 2 && !string.IsNullOrEmpty(cols[1].Value))
            {
                return StringConverter.StringToDecimal(cols[1].Value);
            }

            return null;
        }

        private TransactionDto ReadTransactionItem(XElement element)
        {
            var row = element.Elements().Single();
            var cols = row.Elements().ToArray();
            if (cols.Length != 7 || string.IsNullOrEmpty(cols[0].Value) || cols[0].Value.Trim().Length != 10)
                return null;

            var index = 0;
            var item = new TransactionDto
            {
                RegistrationDate = StringConverter.StringToDateTime(cols[index++].Value),
                TransactionDate = StringConverter.StringToDateTime(cols[index++].Value),
                Code = cols[index++].Value.Trim(),
                Description = cols[index++].Value.Trim(),
                CurrencyType = CurrencyTypeDtos.FirstOrDefault(i => i.ShortName.Equals(cols[index++].Value.Trim())),
                QuantityByTransaction = StringConverter.StringToDecimal(cols[index++].Value),
                QuantityByWallet = StringConverter.StringToDecimal(cols[index].Value)
            };

            return item;
        }
    }
}