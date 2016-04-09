using System;

using LokalReporter.Responses;

using XLabs.Forms.Controls;

using DataTemplateSelector = XLabs.Forms.Controls.DataTemplateSelector;

namespace LokalReporter.App.FormsApp.Statics {

    public static class Converters {
        public static StringToHtmlConverter StringToHtml { get; } = new StringToHtmlConverter();

        public static DataTemplateSelector ArticleCarouselItemTemplateSelector { get; } = new ArticlesCarouselTemplateSelector();

        public static StringToUriConverter StringToUri { get; } = new StringToUriConverter();

        public static Func<object, string> DistrictNameSelector { get; } = o => ((District) o).Name;
    }
}