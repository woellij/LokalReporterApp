using System;
using Android.Content;
using LokalReporter.App.FormsApp;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using Newtonsoft.Json;

namespace LokalReporter.App.Droid {

    public class Setup : MvxAndroidSetup {
        public Setup(Context applicationContext) : base(applicationContext) {}

        protected override IMvxApplication CreateApp()
        {
            return new FormsApp.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFormsDroidPagePresenter = new LokalReporterAppAndroidMvxFormspagePresenter();
            Mvx.RegisterSingleton<IMvxViewPresenter>(mvxFormsDroidPagePresenter);
            return mvxFormsDroidPagePresenter;
        }
        private class LokalReporterAppAndroidMvxFormspagePresenter : LokalReporterAppMvxFormsPagePresenter, IMvxAndroidViewPresenter { }
        
    }
}