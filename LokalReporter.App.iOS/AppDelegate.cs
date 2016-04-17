using System;
using System.Reflection;

using Foundation;

using LokalReporter.App.FormsApp;

using MvvmCross.Core.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;

using NControl.Controls.iOS;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using BindingFlags = System.Reflection.BindingFlags;

namespace LokalReporter.App.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate, IMvxApplicationDelegate
    {

        public event EventHandler<MvxLifetimeEventArgs> LifetimeChanged;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            Forms.Init();
            NControls.Init();


            var xamarinFormsApp = new LokalReporterFormsApp();

            this.LoadApplication(xamarinFormsApp);
            base.FinishedLaunching(application, launchOptions);

            var fieldInfo = typeof(FormsApplicationDelegate).GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);
            var window = fieldInfo.GetValue(this);
            var setup = new Setup(this, (UIWindow) window);
            setup.Initialize();
            Dependencies.Initialize();

            Mvx.Resolve<IMvxAppStart>().Start();
            
            return true;
        }

        public override void WillEnterForeground(UIApplication application)
        {
            this.FireLifetimeChanged(MvxLifetimeEvent.ActivatedFromMemory);
        }

        public override void DidEnterBackground(UIApplication application)
        {
            this.FireLifetimeChanged(MvxLifetimeEvent.Deactivated);
        }

        public override void WillTerminate(UIApplication application)
        {
            this.FireLifetimeChanged(MvxLifetimeEvent.Closing);
        }

        private void FireLifetimeChanged(MvxLifetimeEvent which)
        {
            this.LifetimeChanged?.Invoke(this, new MvxLifetimeEventArgs(which));
        }

    }
}