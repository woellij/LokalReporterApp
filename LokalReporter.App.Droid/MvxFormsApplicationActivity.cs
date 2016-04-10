using Android.App;
using Android.Content.PM;
using Android.OS;

using LokalReporter.App.FormsApp;
using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.App.FormsApp.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using Plugin.Toasts;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace LokalReporter.App.Droid {

    [Activity(Label = "MvxFormsApplicationActivity", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MvxFormsApplicationActivity : FormsAppCompatActivity {

        public static MvxFormsApplicationActivity Current { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            MvxFormsApplicationActivity.Current = this;

            // appCompat stuff
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(bundle);
            Forms.Init(this, bundle);

            var formsApp = new LokalReporterFormsApp();
            this.LoadApplication(formsApp);


            Mvx.LazyConstructAndRegisterSingleton<IToastNotificator,ToastNotificatorImplementation>();
            ToastNotificatorImplementation.Init(Current);

            var presenter = (MvxFormsPagePresenter) Mvx.Resolve<IMvxViewPresenter>();
            presenter.MvxFormsApp = formsApp;

            Dependencies.Initialize();
        }

    }

}