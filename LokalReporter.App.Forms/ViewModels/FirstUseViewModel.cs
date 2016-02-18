using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using PropertyChanged;
using ReactiveUI;
using ReactiveCommand = ReactiveUI.Legacy.ReactiveCommand;

namespace LokalReporter.App.FormsApp.ViewModels {

    [ImplementPropertyChanged]
    public class FirstUseViewModel : BaseViewModel {
        public FirstUseViewModel()
        {
            this.DistrictSetting = (DistrictSettingViewModel) new MvxDefaultViewModelLocator().Load(typeof (DistrictSettingViewModel), null, null);
        }

        public DistrictSettingViewModel DistrictSetting { get; }

        public ReactiveCommand Continue { get; set; }

        public override async void Start()
        {
            base.Start();

            this.Continue =
                new ReactiveCommand(this.WhenAny(model => model.DistrictSetting.SelectedDistrict, change => change.Value != null));
            
            this.Continue.Subscribe(o => this.ShowViewModel<PersonalFeedsViewModel>());
        }
    }

}