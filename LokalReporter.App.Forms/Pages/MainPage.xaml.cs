using System;

using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.App.FormsApp.ViewModels;

using MvvmCross.Core.ViewModels;

using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Pages
{
    public partial class MainPage : NavigationPage
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ProfileMenuItemClicked(object sender, EventArgs e)
        {
            ((IMvxViewModel) this.CurrentPage.BindingContext).ShowViewModel<ProfileViewModel>();
        }

    }
}