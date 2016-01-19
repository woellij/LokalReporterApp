using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Platform;

using Xamarin.Forms;

namespace LokalReporter.App.FormsApp {

    public class LokalReporterFormsApp : MvxFormsApp
    {

        public LokalReporterFormsApp()
        {
            // The root page of your application

            this.MainPage = new ContentPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Mvx.Resolve<IMvxAppStart>().Start();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }

}