using Android.App;
using Android.Content.PM;
using Android.OS;

using MvvmCross.Droid.Views;
using NControl.Controls.Droid;
using Xamarin.Forms;

namespace LokalReporter.App.Droid {

    [Activity(Label = "Die Lokalreporter", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.Splash", 
        NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity {

        public SplashScreen() : base(Resource.Layout.SplashScreen) {}

        protected override void OnCreate(Bundle bundle)
        {
            Forms.Init(this, bundle);
            // Leverage controls' StyleId attrib. to Xamarin.UITest
            Forms.ViewInitialized += (sender, e) => {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId)) {
                    e.NativeView.ContentDescription = e.View.StyleId;
                }
            };
            NControls.Init();

            base.OnCreate(bundle);
        }

        protected override void TriggerFirstNavigate()
        {
            this.StartActivity(typeof (MvxFormsApplicationActivity));
        }

    }

}