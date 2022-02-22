
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Smokeball.WPF.Presentation.ViewTemplate.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        #region Methods
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = (bool)value;

            return isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Hidden;
        } 
        
        #endregion
    }
}
