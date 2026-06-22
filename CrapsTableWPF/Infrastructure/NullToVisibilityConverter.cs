using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CrapsTableWPF.Infrastructure
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type _, object __, CultureInfo ___)
        {
            return value == null
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        public object ConvertBack(object value, Type _, object __, CultureInfo ___)
        {
            throw new NotSupportedException();
        }
    }
}
