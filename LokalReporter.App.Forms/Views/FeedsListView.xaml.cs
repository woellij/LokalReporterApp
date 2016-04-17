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

        public FeedsListView()
        {
            this.InitializeComponent();
            //this.ItemSelected += (sender, args) => this.SelectedItem = null;
        }

    }
}