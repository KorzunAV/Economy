using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CQRS.Logic;
using Economy.Dtos;
using Economy.Helpers;
using Economy.Logic.Commands.SaveListCommands;
using Economy.ViewModels;
using HtmlAgilityPack;

namespace Economy.Parsers
{
    public class BelinvestConverter : MailParser, IConverter
    {
        public BelinvestConverter(ICommandQueryDispatcher commandQueryDispatcher)
            : base(commandQueryDispatcher) { }

        public void ConvertAndSave(string filePath, string outFilePath)
        {
            try
            {
                DoConvertAndSave(filePath, outFilePath);
            }
            catch (Exception ex)
            {
            }
        }

        public void DoConvertAndSave(string filePath, string outFilePath)
        {
            //var t = new BelinvestConverter(CommandQueryDispatcher);
            //t.ConvertAndSave(filePath, outFilePath);

            var user = App.User;

            //CommandQueryDispatcher.ExecuteCommand<WalletDto>(new WalletSaveCommand(new WalletDto()
            //{
            //    Balance = 0,
            //    CurrencyType = new CurrencyTypeDto() { ShortName = "BYR" },
            //    Name = CourseArhiveConverter.BankName,
            //    SystemUserId = user.Id,
            //    Bank = new BankDto() { Name = CourseArhiveConverter.BankName }
            //}));

            var document = new HtmlDocument();
            using (var sr = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
            {
                document.Load(sr);
            }
            var body = document.GetBody();
            CleanFile(body);
            var montlyReport = new MontlyReportDto()
            {
                Wallet = user.Dto.Wallets.SingleOrDefault(i => i.Bank.Name.Equals(CourseArhiveConverter.BankName) && i.CurrencyType.ShortName.Equals("BYR")),
                Transactions = new List<TransactionDto>()
            };
            var tables = body.Descendants("table").ToArray();

            var index = 0;
            ReadTitle(tables[index++], montlyReport);
            ReadHeader(tables[index++], montlyReport);
            decimal sumAll = 0;

            for (var i = index + 1; i < tables.Length - 1; i++)
            {
                var tbl = tables[i];
                var item = ReadTransactionItem(tbl);
                if (item != null)
                {
                    montlyReport.Transactions.Add(item);
                }
                //else
                //{
                //    var sum = Sum(tbl);
                //    if (sum != null)
                //    {
                //        sumAll = sum.Value;
                //    }
                //}
            }
            for (var i = tables.Length - 1; i > 0; i--)
            {
                if (tables[i].InnerHtml.Contains("Конечный остаток"))
                {
                    montlyReport.EndBalance = Sum(tables[i]);
                    break;
                }
            }


            var cur = montlyReport.Transactions.Sum(itm => itm.QuantityByWallet + (itm.Fee ?? 0));

            if (cur + montlyReport.StartBalance != montlyReport.EndBalance)
            {
                throw new ArithmeticException("Суммы не сошлись");
            }

            var montlyReportDtos = new List<MontlyReportDto>();
            var groups = montlyReport.Transactions.OrderBy(i => i.RegistrationDate).GroupBy(i => i.RegistrationDate.Year * 100 + i.RegistrationDate.Month);
            var startBalance = montlyReport.StartBalance;
            foreach (var group in groups)
            {
                var montlyReportByYearMonth = new MontlyReportDto()
                {
                    StartBalance = startBalance,
                    EndBalance = startBalance,
                    StartDate = new DateTime(group.Key / 100, group.Key % 100, 1),
                    Wallet = montlyReport.Wallet
                };

                montlyReportByYearMonth.Transactions = group.ToList();
                montlyReportByYearMonth.EndBalance = montlyReportByYearMonth.Transactions.Sum(i => i.QuantityByWallet.Value);
                montlyReportDtos.Add(montlyReportByYearMonth);
            }

            CommandQueryDispatcher.ExecuteCommand<List<MontlyReportDto>>(new MontlyReportSaveListCommand(montlyReportDtos));
        }

        private void CleanFile(HtmlNode node)
        {
            string[] remove = { "font", "bgcolor", "meta", "hr", "b", "SMS-оповещение", "strong" };

            foreach (var ch in node.ChildNodes.ToArray())
            {
                if (remove.Contains(ch.Name.ToLower()))
                {
                    if (string.IsNullOrWhiteSpace(ch.InnerHtml))
                    {
                        ch.Remove();
                    }
                    else
                    {
                        if (ch.ChildNodes.Any())
                        {
                            CleanFile(ch);
                        }
                        foreach (var chnode in ch.ChildNodes)
                        {
                            ch.ParentNode.InsertBefore(chnode, ch);
                        }
                        ch.Remove();
                    }
                }
                else if (ch.Name.Equals("table", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(StringConverter.GetString(ch.InnerText)))
                {
                    ch.Remove();
                }
                else
                {
                    if (ch.Attributes.Any())
                        ch.Attributes.RemoveAll();
                    if (ch.ChildNodes.Any())
                    {
                        CleanFile(ch);
                    }
                    if (ch is HtmlTextNode && string.IsNullOrEmpty(ch.InnerHtml) || ch.InnerHtml == "\r\n")
                    {
                        ch.Remove();
                    }
                }
            }
        }

        private bool ReadPreambula(HtmlNode table)
        {
            //var titleTable = root.Elements().First();
            //var elem = titleTable.Elements().ToArray();
            //if (elem.Length != 1)
            //{
            //    return false;
            //}
            ////var title = elem[0].Value;
            //titleTable.Remove();
            return true;
        }

        private void ReadTitle(HtmlNode table, MontlyReportDto montlyReport)
        {
            var infoLines = table.Descendants("tr").ToArray();
            if (infoLines.Length != 5)
                throw new ArgumentException("Ожидался набор из 5 элементов!");

            //montlyReport.Wallet.SystemUser.Name = infoLines[0].Elements().Single().Value;
            //montlyReport.WalletDto.BancInfo = infoLines[1].Elements().Single().Value;
            //montlyReport.ReportInfo = infoLines[2].Elements().Single().Value;
            montlyReport.StartDate = GetDate(infoLines[3].InnerHtml);
            //montlyReport.CreationInfo = infoLines[4].Elements().Single().Value;
        }

        private static DateTime GetDate(string line, bool fromEnd = false)
        {
            var m = Regex.Matches(line, "(0[1-9]|[1-2][0-9]|3[0-1])(-|.)(0[1-9]|1[0-2])(-|.)[0-9]{4}( *)([0-2][0-9]:[0-5][0-9]:[0-5][0-9])?");

            var date = fromEnd
                ? m[1].Value
                : m[0].Value;

            return DateTime.Parse(date);
        }

        private void ReadHeader(HtmlNode table, MontlyReportDto montlyReport)
        {
            var infoLines = table.Descendants("tr").ToArray();
            if (infoLines.Length != 4)
                throw new ArgumentException("Ожидался набор из 4 элементов!");

            //montlyReport.ImmutableBalance = StringConverter.StringToDecimal(infoLines[0].Elements().Skip(3).First().Value);
            //montlyReport.Wallet.Name = infoLines[1].Elements().Skip(1).First().Value;
            //montlyReport.MinBalance = StringConverter.StringToDecimal(infoLines[1].Elements().Skip(3).First().Value);
            //var currencyTypeShortName = infoLines[2].Elements().Skip(1).First().Value;
            //montlyReport.Wallet.CurrencyType = CurrencyTypeDtos.FirstOrDefault(i => i.ShortName.Equals(currencyTypeShortName));
            //montlyReport.AvailibleCredit = StringConverter.StringToDecimal(infoLines[2].Elements().Skip(3).First().Value);
            montlyReport.StartBalance = StringConverter.StringToDecimal(infoLines[3].ChildNodes[4].InnerHtml);
        }

        private decimal Sum(HtmlNode table)
        {
            var tr = table.Descendants("tr").ToArray();
            if (tr.Length != 1)
                throw new ArithmeticException("Неожиданное число строк!");

            var td = tr[0].Descendants("td").ToArray();

            if (td.Length == 2 && !string.IsNullOrEmpty(td[1].InnerHtml))
            {
                return StringConverter.StringToDecimal(td[1].InnerHtml);
            }
            throw new ArithmeticException("Неожиданное число колонок или пустое значение конечной суммы!");
        }

        private TransactionDto ReadTransactionItem(HtmlNode table)
        {
            var row = table.Descendants("tr").ToArray();
            var cols = row[0].Descendants("td").ToArray();
            if (cols.Length != 7 || string.IsNullOrEmpty(cols[0].InnerHtml))
                return null;

            var index = 0;
            var item = new TransactionDto();
            item.RegistrationDate = StringConverter.GetDateTime(cols[index++].InnerHtml);
            item.TransactionDate = StringConverter.GetDateTime(cols[index++].InnerHtml);
            item.Code = StringConverter.GetString(cols[index++].InnerHtml);
            item.Description = StringConverter.GetString(cols[index++].InnerHtml);

            var curShortName = StringConverter.GetString(cols[index++].InnerHtml);
            if (!string.IsNullOrEmpty(curShortName))
                item.CurrencyType = new CurrencyTypeDto { ShortName = curShortName };

            item.QuantityByTransaction = StringConverter.StringToDecimal(cols[index++].InnerHtml);
            item.QuantityByWallet = StringConverter.StringToDecimal(cols[index].InnerHtml);
            if (row.Length == 2)
            {
                item.Fee = StringConverter.StringToDecimal(row[1].ChildNodes[6].InnerHtml);
            }

            return item;
        }
    }
}