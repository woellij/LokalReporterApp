using System;
using System.Linq;
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

        //protected override void OnBindingContextChanged()
        //{
        //    base.OnBindingContextChanged();
        //    foreach (var tapGestureRecognizer in this.GestureRecognizers.OfType<TapGestureRecognizer>())
        //    {
        //        tapGestureRecognizer.Tapped += TapGestureRecognizerOnTapped;
        //    }
        //}

        private void TapGestureRecognizerOnTapped(object sender, EventArgs eventArgs)
        {
            
        }

    }
}