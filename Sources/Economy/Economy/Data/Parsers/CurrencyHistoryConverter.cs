using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Economy.Models;

namespace Economy.Data.Parsers
{
    public class CurrencyHistoryConverter : IConverter
    {
        private DateTime ParseDateTime(string value)
        {
            value = value.Trim();
            return DateTime.ParseExact(value, new[] { "dd.MM.yyyy", "dd.MM.yyyy HH:mm" }, new CultureInfo("ru-RU"),
                DateTimeStyles.None);
        }

        public void ConvertAndSave(string filePath, string outFilePath)
        {
            var set = new List<PriceHistory>();
            using (var sr = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null) continue;

                    var date = line.Split('\t');
                    if (date.Length != 7)
                        throw new ArgumentException("Ожидалось 7 колонок");

                    set.Add(new PriceHistory
                    {
                        Date = ParseDateTime(date[0]),
                        Sel = decimal.Parse(date[1]),
                        Buy = decimal.Parse(date[2]),
                        Currency = "USD"
                    });
                    set.Add(new PriceHistory
                    {
                        Date = ParseDateTime(date[0]),
                        Sel = decimal.Parse(date[3]),
                        Buy = decimal.Parse(date[4]),
                        Currency = "EUR"
                    });
                    set.Add(new PriceHistory
                    {
                        Date = ParseDateTime(date[0]),
                        Sel = decimal.Parse(date[5]),
                        Buy = decimal.Parse(date[6]),
                        Currency = "RUB"
                    });
                }
            }

            var model = new Models.History { PriceHistories = set };
            CommonLibs.Serialization.XmlSerialization.Serialize(model, outFilePath, FileMode.OpenOrCreate);
        }
    }
}
