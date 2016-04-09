using LokalReporter.App.FormsApp.Views;

using Xamarin.Forms;

using DataTemplateSelector = XLabs.Forms.Controls.DataTemplateSelector;

namespace LokalReporter.App.FormsApp.Statics
{
    public class ArticlesCarouselTemplateSelector : DataTemplateSelector {
        public override DataTemplate SelectTemplate(object item, BindableObject container)
        {
            return new DataTemplate(() => new BigArticleView {BindingContext = item});
        }
    }
}