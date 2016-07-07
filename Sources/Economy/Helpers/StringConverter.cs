using System;
using System.Globalization;

namespace Economy.Helpers
{
    public static class StringConverter
    {
        public static decimal StringToDecimal(string value)
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
                return 0;

            var sign = value.EndsWith("-") || value.StartsWith("-");
            if (value.Contains(".") && value.Contains(","))
                value = value.Replace(",", "");
            value = value.Replace('.', ',').Replace("+", string.Empty).Replace("-", string.Empty);
            return (sign ? -1 : 1) * decimal.Parse(value, NumberStyles.Any);
        }

        //private DateTime ParseDateTime(string value)
        //{
        //    value = value.Trim();
        //    return DateTime.ParseExact(value, new[] { "dd.MM.yyyy", "dd.MM.yyyy HH:mm" }, new CultureInfo("ru-RU"), DateTimeStyles.None);
        //}

        public static DateTime StringToDateTime(string value)
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
                throw new InvalidCastException("Ожидалась дата");

            DateTime dateTime;
            if (DateTime.TryParseExact(value, new[] { "dd.MM.yyyy", "dd.MM.yyyy HH:mm" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return dateTime;
            }

            throw new InvalidCastException($"Enable cast {value} to DateTime");
        }
    }
}
