using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace DataGridFilterLibrary.Support
{
    public class MyOppositeBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(bool)value)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            return visibility == Visibility.Visible ? true : false;
        }
    }
}
