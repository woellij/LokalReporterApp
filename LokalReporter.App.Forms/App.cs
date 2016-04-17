using LokalReporter.App.FormsApp.ViewModels;

using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform;

namespace LokalReporter.App.FormsApp
{
    public class App : MvxApplication
    {

        public override void Initialize()
        {
            Mvx.LazyConstructAndRegisterSingleton<IMvxViewModelLocator, MvxDefaultViewModelLocator>();

            this.RegisterAppStart(new LokalReporterAppStart());
        }

        private class LokalReporterAppStart : IMvxAppStart
        {

            public void Start(object hint = null)
            {
                var userSettings = Mvx.Resolve<IUserSettings>();

                var startRequest = new MvxViewModelRequest();

                var districtSetting = userSettings?.DistrictSetting?.GetValueAsync()?.Result;
                startRequest.ViewModelType = districtSetting != null ? typeof (PersonalFeedsViewModel) : typeof (FirstUseViewModel);

                Mvx.Resolve<IMvxViewPresenter>().Show(startRequest);
            }

        }

    }
}