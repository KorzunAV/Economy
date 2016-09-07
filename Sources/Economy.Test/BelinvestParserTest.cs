using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Economy.Models;
using NUnit.Framework;
using CommonLibs.Serialization;

namespace Economy.Test
{
    [TestFixture]
    public class BelinvestParserTest
    {
        const string DirPath = @"..\..\..\Economy\Data\Mails\";
        const string DirPathOut = @"..\..\..\Economy\Data\Converted\";

        [Test]
        public void ReadTest()
        {
            var convertedPaths = Directory.GetFiles(DirPathOut);
            var statistic = new List<MontlyReport>();

            foreach (var filePath in convertedPaths)
            {
                var montlyReport = XmlSerialization.Deserialize<MontlyReport>(filePath);
                Assert.IsNotNull(montlyReport);
                statistic.Add(montlyReport);
            }

            var groups = statistic.OrderBy(i => i.CreatedDateTime).GroupBy(i => i.AccountNumber);
            foreach (var itemList in groups)
            {
                DateTime date;
                var balans = itemList.First().PrevBalance;
                foreach (var item in itemList)
                {
                    if (balans == item.PrevBalance)
                    {
                        balans += item.TransactionDtos.Sum(i => i.QuantityByWallet.Value);
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
