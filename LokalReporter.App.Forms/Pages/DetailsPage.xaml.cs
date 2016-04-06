using System.Threading.Tasks;

using LokalReporter.App.FormsApp.ViewModels;

using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Pages
{
    public partial class DetailsPage : ContentPage
    {

        public static readonly BindableProperty IsBookmarkedProperty = BindableProperty.Create("IsBookmarked", typeof (bool), typeof (DetailsPage), default(bool));

        public bool IsBookmarked
        {
            get { return (bool) this.GetValue(IsBookmarkedProperty); }
            set { this.SetValue(IsBookmarkedProperty, value); }
        }

        public DetailsPage()
        {
            this.InitializeComponent();
            this.ToolbarItems.Remove(this.BookmarkItem);
            this.ToolbarItems.Remove(this.BookmarkedItem);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsBookmarkedProperty.PropertyName)
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
        }


        protected override async void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var content = ((DetailsViewModel) this.BindingContext).Article.HtmlContent;
            var html = (string) Statics.Converters.StringToHtml.Convert(content, null, null, null);
            var htmlWebViewSource = new HtmlWebViewSource
            {
                Html = html
            };
            
            var webView = new WebView {Source = htmlWebViewSource};
            webView.WidthRequest = this.Width;
            this.ContentLayout.Children.Add(webView);

            await Task.Delay(2000);
            this.ToolbarItems.Remove(this.BookmarkItem);
        }

    }
}