using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Economy.Common.Belinvest;
using Economy.Common.FileSystem;
using Economy.Models;
using Economy.ViewModels;
using NUnit.Framework;

namespace Economy.Test
{
    [TestFixture]
    public class BelinvestParserTest
    {
        const string DirPath = @"..\..\..\Economy\Data\Mails\";
        const string DirPathOut = @"..\..\..\Economy\Data\Converted\";
        private readonly Parser _parser = new Parser();

        [Test]
        public void ConvertTest()
        {
            var paths = Directory.GetFiles(DirPath);
            var convertedPaths = Directory.GetFiles(DirPathOut);

            foreach (var filePath in paths)
            {
                if (!convertedPaths.Any(f => Path.GetFileNameWithoutExtension(f) == Path.GetFileNameWithoutExtension(filePath)))
                {
                    var data = _parser.Parse(filePath);
                    if (data != null)
                    {
                        var outpath = Path.Combine(DirPathOut, Path.GetFileNameWithoutExtension(filePath) + ".xml");
                        Serialization.Save(data, outpath, FileMode.Create);
                    }
                }
            }
        }

        [Test]
        public void TryConvertTest()
        {
            var paths = Directory.GetFiles(DirPath);
            var convertedPaths = Directory.GetFiles(DirPathOut);

            foreach (var filePath in paths)
            {
                if (convertedPaths.All(f => Path.GetFileNameWithoutExtension(f) == Path.GetFileNameWithoutExtension(filePath)))
                {
                    var data = _parser.TryParse(filePath);
                    if (data != null)
                    {
                        var outpath = Path.Combine(DirPathOut, Path.GetFileNameWithoutExtension(filePath) + ".xml");
                        Serialization.Save(data, outpath, FileMode.Create);
                    }
                }
            }
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
    }
}
