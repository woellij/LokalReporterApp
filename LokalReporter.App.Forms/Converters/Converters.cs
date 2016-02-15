using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Converters {

    public static class Converters {
        public static IValueConverter StringToHtml { get; } = new StringToHtmlConverter();
    }

}