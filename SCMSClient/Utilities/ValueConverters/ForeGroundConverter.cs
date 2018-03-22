using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SCMSClient.Utilities
{
    public class ForeGroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var targetElement = values[0] as FrameworkElement;
            var styleName = values[1] as string;

            if (styleName == null)
                return null;

            return (Brush)targetElement.TryFindResource(styleName);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}