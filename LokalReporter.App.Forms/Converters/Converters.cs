using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Converters {

    public static class Converters {
        public static StringToHtmlConverter StringToHtml { get; } = new StringToHtmlConverter();
    }

}