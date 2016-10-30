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

namespace Economy.Parsers
{
    public class CourseArhiveConverter : IConverter
    {
        protected ICommandQueryDispatcher CommandQueryDispatcher;

        public CourseArhiveConverter(ICommandQueryDispatcher commandQueryDispatcher)
        {
            CommandQueryDispatcher = commandQueryDispatcher;
        }

        public void ConvertAndSave(string filePath, string outFilePath)
        {
            var currencyTypeDtos = CommandQueryDispatcher.ExecuteQuery<List<CurrencyTypeDto>>(new CurrencyTypeGetAllQuery()).Data;
            var lastCourseArhive = CommandQueryDispatcher.ExecuteQuery<CourseArhiveDto>(new CourseArhiveGetLastQuery()).Data;

            var set = new List<CourseArhiveDto>();
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
                    if (lastCourseArhive != null && dt <= lastCourseArhive.RegDate)
                        break;

                    TryAddItem(dt, date, 1, currencyTypeDtos, "USD", set);
                    TryAddItem(dt, date, 3, currencyTypeDtos, "EUR", set);
                    TryAddItem(dt, date, 5, currencyTypeDtos, "RUB", set);
                }
            }

            if (set.Any())
            {
                //var command = new CourseArhiveSaveCommand { Dto = set };
                //CommandQueryDispatcher.ExecuteCommand<bool>(command);
            }
        }

        private void TryAddItem(DateTime dt, string[] date, int from, List<CurrencyTypeDto> currencyTypeDtos, string currencyTypeShortName, List<CourseArhiveDto> set)
        {
            var itm = new CourseArhiveDto
            {
                RegDate = dt,
                Sel = StringConverter.StringToDecimal(date[from++]),
                Buy = StringConverter.StringToDecimal(date[from]),
                CurrencyType = currencyTypeDtos.FirstOrDefault(i => i.ShortName == currencyTypeShortName)
            };
            if (!set.Any(i => i.RegDate == itm.RegDate && i.CurrencyType.Id == itm.CurrencyType.Id))
                set.Add(itm);
        }
    }
}