using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Views
{
    public partial class FeedsListView
    {

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof (object), typeof (FeedsListView), default(object));

        public object ItemsSource
        {
            get { return (object) this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty FooterTemplateProperty = BindableProperty.Create("FooterTemplate", typeof (DataTemplate), typeof (FeedsListView), default(DataTemplate));

        public DataTemplate FooterTemplate
        {
            get { return (DataTemplate) this.GetValue(FooterTemplateProperty); }
            set { this.SetValue(FooterTemplateProperty, value); }
        }

        public static readonly BindableProperty FooterProperty = BindableProperty.Create("Footer", typeof (object), typeof (FeedsListView), default(object));

        public object Footer
        {
            get { return (object) this.GetValue(FooterProperty); }
            set { this.SetValue(FooterProperty, value); }
        }

        public FeedsListView()
        {
            this.InitializeComponent();
            //this.ItemSelected += (sender, args) => this.SelectedItem = null;
        }

    }
}