using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CQRS.Logic;
using Economy.Dtos;
using Economy.Helpers;
using Economy.Logic.Commands;
using Economy.Logic.Queries;

namespace Economy.Data.Parsers
{
    public class BelinvestCourseArhiveConverter : IConverter
    {
        protected ICommandQueryDispatcher CommandQueryDispatcher;

        public BelinvestCourseArhiveConverter(ICommandQueryDispatcher commandQueryDispatcher)
        {
            CommandQueryDispatcher = commandQueryDispatcher;
        }

        public void ConvertAndSave(string filePath, string outFilePath)
        {
            var currencyTypeDtos = CommandQueryDispatcher.ExecuteQuery<List<CurrencyTypeDto>>(new CurrencyTypeGetAllQuery()).Data;
            var lastBelinvestCourseArhive = CommandQueryDispatcher.ExecuteQuery<BelinvestCourseArhiveDto>(new BelinvestCourseArhiveGetLastQuery()).Data;

            var set = new List<BelinvestCourseArhiveDto>();
            using (var sr = new StreamReader(filePath, Encoding.GetEncoding("windows-1251")))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null) continue;

                    var date = line.Split('\t');
                    if (date.Length != 7)
                        throw new ArgumentException("Ожидалось 7 колонок");

                    var dt = StringConverter.StringToDateTime(date[0]);
                    if (lastBelinvestCourseArhive != null && dt <= lastBelinvestCourseArhive.RegDate)
                        break;

                    TryAddItem(dt, date, 1, currencyTypeDtos, "USD", set);
                    TryAddItem(dt, date, 3, currencyTypeDtos, "EUR", set);
                    TryAddItem(dt, date, 5, currencyTypeDtos, "RUB", set);
                }
            }

            if (set.Any())
            {
                var command = new BelinvestCourseArhiveSaveCommand { Dtos = set };
                CommandQueryDispatcher.ExecuteCommand<bool>(command);
            }
        }

        private void TryAddItem(DateTime dt, string[] date, int from, List<CurrencyTypeDto> currencyTypeDtos, string currencyTypeShortName, List<BelinvestCourseArhiveDto> set)
        {
            var itm = new BelinvestCourseArhiveDto
            {
                RegDate = dt,
                Sel = StringConverter.StringToDecimal(date[from++]),
                Buy = StringConverter.StringToDecimal(date[from]),
                CurrencyTypeDto = currencyTypeDtos.FirstOrDefault(i => i.ShortName == currencyTypeShortName)
            };
            if (!set.Any(i => i.Equals(itm)))
                set.Add(itm);
        }
    }
}