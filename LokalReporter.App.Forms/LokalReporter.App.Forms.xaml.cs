﻿using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.App.FormsApp.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Platform;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LokalReporter.App.FormsApp {
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LokalReporterFormsApp : MvxFormsApp {
        public LokalReporterFormsApp()
        {
            this.InitializeComponent();
            // The root page of your application

            this.MainPage = new ContentPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            var presenter = Mvx.Resolve<IMvxViewPresenter>();
            var vmType = Settings.SelectedDistrict == null ? typeof (FirstUseViewModel) : typeof (PersonalFeedsViewModel);
            presenter.Show(new MvxViewModelRequest(vmType, null, null, null));

            Mvx.LazyConstructAndRegisterSingleton<IMvxViewModelLocator, MvxDefaultViewModelLocator>();
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