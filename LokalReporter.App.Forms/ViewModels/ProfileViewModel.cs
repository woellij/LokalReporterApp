using LokalReporter.App.FormsApp.Helpers;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace LokalReporter.App.FormsApp.ViewModels {

    public class ProfileViewModel : BaseViewModel {
        public override void Start()
        {
            base.Start();
            var loader = Mvx.Resolve<IMvxViewModelLocator>();
            this.DistrictSetting = loader.LoadViewModel<DistrictSettingViewModel>();
            this.FiltersSetting = loader.LoadViewModel<FilterSettingsViewModel>();
        }

        public FilterSettingsViewModel FiltersSetting { get; set; }

        public DistrictSettingViewModel DistrictSetting { get; set; }
    }

}