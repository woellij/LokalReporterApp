using System;
using System.Globalization;

using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Statics
{
    public class StringToUriConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Uri((string) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Uri) value).ToString();
        }
    }
}