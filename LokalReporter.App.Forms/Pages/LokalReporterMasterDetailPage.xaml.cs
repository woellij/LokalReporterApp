using System;

using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.App.FormsApp.ViewModels;

using MvvmCross.Core.ViewModels;

using Xamarin.Forms;

namespace LokalReporter.App.FormsApp.Pages
{
    public partial class LokalReporterMasterDetailPage : MasterDetailPage
    {

        public LokalReporterMasterDetailPage()
        {
            this.InitializeComponent();
        }

        private void ProfileMenuItemClicked(object sender, EventArgs e)
        {
            var currentPage = (this.Detail as NavigationPage).CurrentPage;
            ((IMvxViewModel)currentPage.BindingContext).ShowViewModel<ProfileViewModel>();
        }

    }
}