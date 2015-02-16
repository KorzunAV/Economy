using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Economy.Common.Belinvest;
using Economy.Common.FileSystem;
using Economy.Data;
using Economy.Models;
using NUnit.Framework;

namespace Economy.Test
{
    [TestFixture]
    public class BelinvestParserTest
    {
        const string DirPath = @"..\..\..\Economy\Data\Mails\";
        const string DirPathOut = @"..\..\..\Economy\Data\Converted\";

        [Test]
        public void ConvertTest()
        {
            var manager = new ConvertManager();
            manager.Convert(DirPath, DirPathOut);
        }

        [Test]
        public void TryConvertTest()
        {
            var manager = new ConvertManager();
            manager.TryConvert(DirPath, DirPathOut);
        }

        [Test]
        public void ReadTest()
        {
            var convertedPaths = Directory.GetFiles(DirPathOut);
            var statistic = new List<MontlyReport>();

            foreach (var filePath in convertedPaths)
            {
                var montlyReport = Deserialization.Load<MontlyReport>(filePath);
                Assert.IsNotNull(montlyReport);
                statistic.Add(montlyReport);
            }

            var groups = statistic.OrderBy(i => i.CreatedDateTime).GroupBy(i => i.AccountNumber);
            foreach (var itemList in groups)
            {
                DateTime date;
                decimal balans = itemList.First().PrevBalance;
                foreach (var item in itemList)
                {
                    if (balans == item.PrevBalance)
                    {
                        balans += item.TransactionItems.Sum(i=>i.AmountByAccount);
                    }
                    else
                    {
                        Assert.Pass("Дебит с кредитом не срослись");
                    }
                }
            }
        }

        [Test]
        public void ConvertHistory()
        {
            
        }
    }
}
