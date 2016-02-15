using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalReporter.App.FormsApp.ViewModels;
using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Pages
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var content = ((DetailsViewModel) this.BindingContext).Article.HtmlContent.InnerHtml;
            var html = (string) Converters.Converters.StringToHtml.Convert(content, null, null, null);
            var htmlWebViewSource = new HtmlWebViewSource {
                Html = html
            };

            var webView = new WebView();
            webView.Source = htmlWebViewSource;
            this.Content = webView;
        }
    }
}
