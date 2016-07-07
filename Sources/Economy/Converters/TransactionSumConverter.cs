using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Economy.Converters
{
    public class TransactionSumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var typedValue = (ICollection)value;
            if (typedValue.Count > 0)
            {
                return Double.NaN;
            }
            else
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
