using System;
using System.Globalization;
using LokalReporter.App.FormsApp.Views;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace LokalReporter.App.FormsApp.Converters {

    public static class Converters {
        public static StringToHtmlConverter StringToHtml { get; } = new StringToHtmlConverter();

        public static DataTemplateSelector ArticleCarouselItemTemplateSelector { get; } = new ArticlesCarouselTemplateSelector();

        public static StringToUriConverter StringToUri { get; } = new StringToUriConverter();
    }

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

    public class ArticlesCarouselTemplateSelector : DataTemplateSelector {
        public override DataTemplate SelectTemplate(object item, BindableObject container)
        {
            return new DataTemplate(() => new BigArticleView {BindingContext = item});
        }
    }

}