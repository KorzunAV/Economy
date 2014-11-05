using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Economy.Common.Belinvest;
using Economy.Common.FileSystem;
using Economy.Models;
using NUnit.Framework;

namespace Economy.Test
{
    [TestFixture]
    public class BelinvestParserTest
    {
        [Test]
        public void CleanTest()
        {
            const string filePath = @"D:/Projects/MyProjects/Economy/Economy/Sources/Economy/Economy/Data/Mails/10239140.htm";
            var data = Parser.Parse(filePath);
            Assert.IsNotNull(data);
        }

        [Test]
        public void ConvertTest()
        {
            const string dirPath = @"D:\Projects\MyProjects\Economy\Economy\Sources\Economy\Economy\Data\Mails\";
            const string dirPathOut = @"D:\Projects\MyProjects\Economy\Economy\Sources\Economy\Economy\Data\Converted\";
            var paths = Directory.GetFiles(dirPath);
            var convertedPaths = Directory.GetFiles(dirPathOut);

            foreach (var filePath in paths)
            {
                if (!convertedPaths.Any(f => Path.GetFileNameWithoutExtension(f) == Path.GetFileNameWithoutExtension(filePath)))
                {
                    var data = Parser.Parse(filePath);
                    if (data != null)
                    {
                        var outpath = Path.Combine(dirPathOut, Path.GetFileNameWithoutExtension(filePath) + ".xml");
                        Serialization.Save(data, outpath, FileMode.Create);
                    }
                }
            }
        }

        [Test]
        public void TryConvertTest()
        {
            const string dirPath = @"D:\Projects\MyProjects\Economy\Economy\Sources\Economy\Economy\Data\Mails\";
            const string dirPathOut = @"D:\Projects\MyProjects\Economy\Economy\Sources\Economy\Economy\Data\Converted\";
            var paths = Directory.GetFiles(dirPath);
            var convertedPaths = Directory.GetFiles(dirPathOut);

            foreach (var filePath in paths)
            {
                if (convertedPaths.All(f => Path.GetFileNameWithoutExtension(f) == Path.GetFileNameWithoutExtension(filePath)))
                {
                    var data = Parser.TryParse(filePath);
                    if (data != null)
                    {
                        var outpath = Path.Combine(dirPathOut, Path.GetFileNameWithoutExtension(filePath) + ".xml");
                        Serialization.Save(data, outpath, FileMode.Create);
                    }
                }
            }
        }

        [Test]
        public void ReadTest()
        {
            const string dirPathOut = @"D:\Projects\MyProjects\Economy\Economy\Sources\Economy\Economy\Data\Converted\";
            var convertedPaths = Directory.GetFiles(dirPathOut);
            var statistic = new List<MontlyReport>();

            foreach (var filePath in convertedPaths)
            {
                var montlyReport = Deserialization.Load<MontlyReport>(filePath);
                Assert.IsNotNull(montlyReport);
                statistic.Add(montlyReport);
            }

            var groups = statistic.OrderBy(i => i.LastDate).GroupBy(i => i.AccountNumber);
            foreach (var itemList in groups)
            {
                DateTime date;
                decimal balans = 0;
                foreach (var item in itemList)
                {
                    if (balans == item.PrevBalance)
                    {
                        balans += item.TransactionItems.Sum(i=>i.AmountByAccount);
                    }
                }
            }
        }
    }
}
