using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CQRS.Logic;
using Economy.Dtos;
using Economy.Helpers;
using Economy.Models;

namespace Economy.Parsers
{
    public class PriorConverter : MailParser, IConverter
    {
        public PriorConverter(ICommandQueryDispatcher commandQueryDispatcher)
           : base(commandQueryDispatcher) { }

        public void ConvertAndSave(string filePath, string outFilePath)
        {
            string file;
            using (var sr = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
            {
                file = sr.ReadToEnd();
            }

            var lines = file.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var i = 0;
            var montlyReport = new MontlyReport();
            montlyReport.TransactionDtos = new List<TransactionDto>();
            montlyReport.UserInfo = lines[i++];
            montlyReport.BancInfo = lines[i++];
            montlyReport.ReportInfo = lines[i++];
            montlyReport.PeriodInfo = lines[i++];
            montlyReport.CreationInfo = lines[i++];

            montlyReport.ImmutableBalance = StringConverter.StringToDecimal(lines[i++]);
            montlyReport.AccountNumber = lines[i++];
            montlyReport.MinBalance = StringConverter.StringToDecimal(lines[i++]);
            montlyReport.AccountCurrency = CurrencyTypeDtos.FirstOrDefault(itm => itm.ShortName.Equals(lines[i]));
            i++;
            montlyReport.AvailibleCredit = StringConverter.StringToDecimal(lines[i++]);
            montlyReport.PrevBalance = StringConverter.StringToDecimal(lines[i++]);

            while (i < lines.Length - 1)
            {
                var item = GetTransactionItem(lines[i++]);
                montlyReport.TransactionDtos.Add(item);
            }

            var sumAll = StringConverter.StringToDecimal(lines[i]);

            var cur = montlyReport.TransactionDtos.Sum(itm => itm.QuantityByWallet);

            if (cur + montlyReport.PrevBalance != sumAll)
            {
                throw new ArithmeticException("Суммы не сошлись");
            }

            //CommonLibs.Serialization.XmlSerialization.Serialize(montlyReport, outFilePath, FileMode.OpenOrCreate);
        }

        private TransactionDto GetTransactionItem(string line)
        {
            var items = line.Split('\t');
            int i = 0;
            var rez = new TransactionDto();
            rez.TransactionDate = StringConverter.GetDateTime(items[i++]);
            rez.Description = items[i++].Trim();
            rez.QuantityByTransaction = StringConverter.StringToDecimal(items[i++]);
            rez.CurrencyType = CurrencyTypeDtos.FirstOrDefault(itm => itm.ShortName.Equals(items[i].Trim()));
            i++;
            rez.RegistrationDate = StringConverter.GetDateTime(items[i++]);
            rez.QuantityByWallet = StringConverter.StringToDecimal(items[i]);
            return rez;
        }

    }
}