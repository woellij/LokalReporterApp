using System.Windows.Input;

using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Views
{
    public partial class SimpleArticleView
    {

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof (ICommand), typeof (SimpleArticleView), default(ICommand));

        public ICommand Command
        {
            get { return (ICommand) this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }
        public SimpleArticleView()
        {
            this.InitializeComponent();
        }

    }
}