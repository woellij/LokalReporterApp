using System.Threading.Tasks;

using LokalReporter.App.FormsApp.Statics;
using LokalReporter.App.FormsApp.ViewModels;

using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Pages
{
    public partial class DetailsPage : ContentPage
    {

        public static readonly BindableProperty IsBookmarkedProperty = BindableProperty.Create("IsBookmarked", typeof (bool), typeof (DetailsPage), default(bool));
        private WebView webView;

        public DetailsPage()
        {
            this.InitializeComponent();
            this.ToolbarItems.Remove(this.BookmarkItem);
            this.ToolbarItems.Remove(this.BookmarkedItem);
        }

        public bool IsBookmarked
        {
            get { return (bool) this.GetValue(IsBookmarkedProperty); }
            set { this.SetValue(IsBookmarkedProperty, value); }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsBookmarkedProperty.PropertyName)
            {
                this.AdjustBookmarkedState();
            }
        }

        private void AdjustBookmarkedState()
        {
            this.ToolbarItems.Remove(this.BookmarkItem);
            this.ToolbarItems.Remove(this.BookmarkedItem);
            if (this.IsBookmarked)
            {
                this.ToolbarItems.Add(this.BookmarkedItem);
            }
            else
            {
                this.ToolbarItems.Add(this.BookmarkItem);
            }
            foreach (var toolbarItem in this.ToolbarItems)
            {
                toolbarItem.BindingContext = this.BindingContext;
            }
        }


        protected override async void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            this.AdjustBookmarkedState();

            var content = ((DetailsViewModel) this.BindingContext).Article.HtmlContent;
            var html = (string) Converters.StringToHtml.Convert(content, null, null, null);
            var htmlWebViewSource = new HtmlWebViewSource
            {
                Html = html
            };

            this.webView = new WebView {Source = htmlWebViewSource};
            this.webView.VerticalOptions = LayoutOptions.FillAndExpand;
            this.webView.HorizontalOptions = LayoutOptions.Fill;

            this.ContentLayout.Children.Add(this.webView);

            await Task.Delay(2000);
        }

    }
}