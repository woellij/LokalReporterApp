using System;

using LokalReporter.App.FormsApp;
using LokalReporter.App.FormsApp.Presenter;

using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Presenter.iOS;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

using UIKit;

using Xamarin.Forms;

namespace LokalReporter.App.iOS
{
    public class Setup : MvxIosSetup
    {
        
        public Setup(IMvxApplicationDelegate applicationDelegate, UIWindow window) : base(applicationDelegate, window)
        {
        }

        public Setup(IMvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter) : base(applicationDelegate, presenter)
        {
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxApplication CreateApp()
        {
            return new FormsApp.App();
        }

        protected override IMvxIosViewPresenter CreatePresenter()
        {
            var presenter = new LokalReporterAppIosMvxFormspagePresenter(this.Window, LokalReporterFormsApp.Instance);
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);
            return presenter;
        }


        private class LokalReporterAppIosMvxFormspagePresenter : LokalReporterAppMvxFormsPagePresenter, IMvxIosViewPresenter
        {
            private readonly UIWindow window;

            public LokalReporterAppIosMvxFormspagePresenter(UIWindow window, LokalReporterFormsApp xamarinFormsApp)
                :base(xamarinFormsApp)
            {
                this.window = window;
            }

            public bool PresentModalViewController(UIViewController controller, bool animated)
            {
                throw new NotImplementedException();
            }

            public void NativeModalViewControllerDisappearedOnItsOwn()
            {
                throw new NotImplementedException();
            }

            protected override void CustomPlatformInitialization(NavigationPage mainPage)
            {
                this.window.RootViewController = mainPage.CreateViewController();
            }

        }

    }
}