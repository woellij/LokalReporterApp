using System.Windows.Input;

using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Views
{
    public partial class SimpleFeed
    {

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof (object), typeof (SimpleFeed), default(object));


        public static readonly BindableProperty ItemClickCommandProperty = BindableProperty.Create("ItemClickCommand", typeof (ICommand), typeof (SimpleFeed), default(ICommand));

        public SimpleFeed()
        {
            this.InitializeComponent();
        }

        public object ItemsSource
        {
            get { return this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public ICommand ItemClickCommand
        {
            get { return (ICommand) this.GetValue(ItemClickCommandProperty); }
            set { this.SetValue(ItemClickCommandProperty, value); }
        }

    }
}