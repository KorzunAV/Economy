using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Economy.Models;

namespace Economy.Common.Belinvest
{
    public class Parser : MailParser, IParser
    {
        public object TryParse(string filePath)
        {
            try
            {
                return Parse(filePath);
            }
            catch (Exception)
            {
            }
            return null;
        }

        public object Parse(string filePath)
        {
            string file;
            using (var sr = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
            {
                file = sr.ReadToEnd();
            }
            file = CleanFile(file);
            var tree = ToDocument(file);
            var montlyReport = new MontlyReport();

            var result = ReadPreambula(tree.Root);

            if (result)
                result = ReadTitle(tree.Root, montlyReport);
            if (result)
                result = ReadHeader(tree.Root, montlyReport);
            if (result)
                result = ReadTransactionItems(tree.Root, montlyReport);

            if (!result)
                throw new ArithmeticException("Где то несраслось.");

            return montlyReport;
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

        private bool ReadTitle(XElement root, MontlyReport montlyReport)
        {
            var titleTable = root.Elements().First();
            var infoLines = titleTable.Elements().ToArray();
            if (infoLines.Length != 5)
                return false;

            montlyReport.UserInfo = infoLines[0].Elements().Single().Value;
            montlyReport.BancInfo = infoLines[1].Elements().Single().Value;
            montlyReport.ReportInfo = infoLines[2].Elements().Single().Value;
            montlyReport.PeriodInfo = infoLines[3].Elements().Single().Value;
            montlyReport.CreationInfo = infoLines[4].Elements().Single().Value;
            titleTable.Remove();
            return true;
        }

        private bool ReadHeader(XElement root, MontlyReport montlyReport)
        {
            var hederTable = root.Elements().First();
            var infoLines = hederTable.Elements().ToArray();
            if (infoLines.Length != 4)
                return false;

            montlyReport.ImmutableBalance = StringToDecimal(infoLines[0].Elements().Skip(3).First().Value);
            montlyReport.AccountNumber = infoLines[1].Elements().Skip(1).First().Value;
            montlyReport.MinBalance = StringToDecimal(infoLines[1].Elements().Skip(3).First().Value);
            montlyReport.AccountCurrency = infoLines[2].Elements().Skip(1).First().Value;
            montlyReport.AvailibleCredit = StringToDecimal(infoLines[2].Elements().Skip(3).First().Value);
            montlyReport.PrevBalance = StringToDecimal(infoLines[3].Elements().Skip(3).First().Value);
            hederTable.Remove();
            return true;
        }

        private bool ReadTransactionItems(XElement root, MontlyReport montlyReport)
        {
            var items = root.Elements().ToArray();
            montlyReport.TransactionItems = new List<TransactionItem>();
            decimal sumAll = 0;

            for (int i = 1; i < items.Length - 1; i++)
            {
                var item = ReadTransactionItem(items[i]);
                if (item != null)
                {
                    montlyReport.TransactionItems.Add(item);
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

            var cur = montlyReport.TransactionItems.Sum(itm => itm.AmountByAccount);
            if (cur + montlyReport.PrevBalance != sumAll)
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
                return StringToDecimal(cols[1].Value);
            }

            return null;
        }

        private TransactionItem ReadTransactionItem(XElement element)
        {
            var row = element.Elements().Single();
            var cols = row.Elements().ToArray();
            if (cols.Length != 7 || string.IsNullOrEmpty(cols[0].Value) || cols[0].Value.Trim().Length != 10)
                return null;

            int index = 0;
            var item = new TransactionItem
            {
                RegistrationDate = StringToDateTime(cols[index++].Value),
                TransactionDate = StringToDateTime(cols[index++].Value),
                TransactionCode = cols[index++].Value.Trim(),
                Description = cols[index++].Value.Trim(),
                Currency = cols[index++].Value.Trim(),
                AmountByCurrency = StringToDecimal(cols[index++].Value),
                AmountByAccount = StringToDecimal(cols[index].Value)
            };

            return item;
        }

        private decimal StringToDecimal(string value)
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
                return 0;

            var sign = value.EndsWith("+");
            value = value.Remove(value.Length - 1);
            value = value.Replace('.', ',');
            return (sign ? 1 : -1) * decimal.Parse(value, NumberStyles.Any);
        }

        private DateTime StringToDateTime(string value)
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
                throw new InvalidCastException("Ожидалась дата");

            var dateTime = DateTime.Parse(value);
            return dateTime;
        }

    }
}
