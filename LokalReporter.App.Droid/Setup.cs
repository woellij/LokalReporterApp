using Android.Content;

using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Presenter.Droid;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

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
            var mvxFormsDroidPagePresenter = new MvxFormsDroidPagePresenter();
            Mvx.RegisterSingleton<IMvxViewPresenter>(mvxFormsDroidPagePresenter);
            return mvxFormsDroidPagePresenter;
        }

    }

}