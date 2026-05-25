using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CrapsTableWPF.Infrastructure
{
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type _, object __, CultureInfo ___)
        {
            if (value is bool b)
            {
                return b
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type _, object __, CultureInfo ___)
        {
            if (value is Visibility v)
            {
                return v != Visibility.Visible;
            }

            return true;
        }
    }
}