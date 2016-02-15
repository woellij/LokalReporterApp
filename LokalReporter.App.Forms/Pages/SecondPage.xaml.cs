using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LokalReporter.App.FormsApp.ViewModels;
using LokalReporter.Responses;
using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Pages
{
    public partial class SecondPage : ContentPage
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Article parameter = e.Item as Article ?? (e.Item as BindableObject)?.BindingContext as Article;
            ((SecondViewModel)this.BindingContext).ShowDetails.Execute(parameter);
        }
    }
}
