using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.App.FormsApp.Presenter;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace LokalReporter.App.FormsApp.ViewModels {

    public class ProfileViewModel : BaseViewModel, INavigatedToAware {
        public FilterSettingsViewModel FiltersSetting { get; set; }

        public DistrictSettingViewModel DistrictSetting { get; set; }

        public void OnNavigatedTo(NavigationEventType type)
        {
            if (type == NavigationEventType.Popped) {
                this.Start();
            }
        }

        public override void Start()
        {
            base.Start();
            var loader = Mvx.Resolve<IMvxViewModelLocator>();

            this.DistrictSetting = loader.LoadViewModel<DistrictSettingViewModel>();
            this.FiltersSetting = loader.LoadViewModel<FilterSettingsViewModel>();
            this.Title = "Profil";
        }
    }

}