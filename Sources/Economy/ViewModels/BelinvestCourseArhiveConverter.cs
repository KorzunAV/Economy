using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CQRS.Logic;
using Economy.Dtos;
using Economy.Helpers;
using Economy.Logic.Commands.SaveCommands;
using Economy.Logic.Commands.SaveListCommands;
using Economy.Logic.Queries;
using Economy.Parsers;

namespace Economy.ViewModels
{
    public class CourseArhiveConverter : IConverter
    {
        public const string BankName = "Белинвестбанк";

        protected ICommandQueryDispatcher CommandQueryDispatcher;

        public CourseArhiveConverter(ICommandQueryDispatcher commandQueryDispatcher)
        {
            CommandQueryDispatcher = commandQueryDispatcher;
        }

        public void ConvertAndSave(string filePath, string outFilePath)
        {
            var bankDto = CommandQueryDispatcher.ExecuteCommand<BankDto>(new BankSaveCommand(new BankDto { Name = BankName })).Data;
            var lastDate = CommandQueryDispatcher.ExecuteQuery<DateTime?>(new CourseArhiveGetLastDataQuery(bankDto)).Data;

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

                    var dt = StringConverter.GetDateTime(date[0]);
                    if (lastDate != null && dt <= lastDate)
                        break;

                    TryAddItem(dt, date, 1, "USD", bankDto, set);
                    TryAddItem(dt, date, 3, "EUR", bankDto, set);
                    TryAddItem(dt, date, 5, "RUB", bankDto, set);
                }
            }

            if (set.Any())
            {
                CommandQueryDispatcher.ExecuteCommand<List<CourseArhiveDto>>(new CourseArhiveSaveListCommand(set));
            }
        }

        private void TryAddItem(DateTime dt, string[] date, int from, string currencyTypeShortName, BankDto bankDto, List<CourseArhiveDto> set)
        {
            var itm = new CourseArhiveDto
            {
                RegDate = dt,
                Sel = StringConverter.StringToDecimal(date[from++]),
                Buy = StringConverter.StringToDecimal(date[from]),
                CurrencyType = new CurrencyTypeDto { ShortName = currencyTypeShortName },
                Bank = bankDto
            };
            if (!set.Any(i => i.RegDate == itm.RegDate && i.CurrencyType.Id == itm.CurrencyType.Id))
                set.Add(itm);
        }
    }
}