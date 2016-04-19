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

using NControl.Controls.Droid;

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

            Forms.Init(this, bundle);
            // appCompat stuff
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(bundle);

            // Leverage controls' StyleId attrib. to Xamarin.UITest
            Forms.ViewInitialized += (sender, e) => {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId))
                {
                    e.NativeView.ContentDescription = e.View.StyleId;
                }
            };
            NControls.Init();

            var lokalReporterFormsApp = new LokalReporterFormsApp();
            this.LoadApplication(lokalReporterFormsApp);
            
            var presenter = (MvxFormsPagePresenter) Mvx.Resolve<IMvxViewPresenter>();
            presenter.MvxFormsApp = lokalReporterFormsApp;

            Dependencies.Initialize(Current);

            Mvx.Resolve<IMvxAppStart>().Start();
        }

    }

}