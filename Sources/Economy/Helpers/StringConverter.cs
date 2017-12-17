using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Economy.Helpers
{
    public static class StringConverter
    {
        private static Regex CleanNumR = new Regex("[^0-9.,+-]");
        private static Regex CleanDate = new Regex("[^0-9-:. ]|(^ )|( $)");
        private static Regex CleanString = new Regex("(&nbsp;)|(\n)|(\r)");

        public static decimal StringToDecimal(string value)
        {
            value = CleanNumR.Replace(value, string.Empty);
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

        public static DateTime GetDateTime(string value)
        {
            value = CleanDate.Replace(value, string.Empty);
            if (string.IsNullOrEmpty(value))
                throw new InvalidCastException("Ожидалась дата");

            DateTime dateTime;
            if (DateTime.TryParseExact(value, new[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd.MM.yyyy HH:mm" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return dateTime;
            }

            throw new InvalidCastException($"Enable cast {value} to DateTime");
        }

        internal static string GetString(string innerHtml)
        {
            return CleanString.Replace(innerHtml, " ").Trim();
        }
    }
}
