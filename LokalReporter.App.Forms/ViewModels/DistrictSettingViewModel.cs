using System.Collections.Generic;
using System.Linq;

using LokalReporter.Common;
using LokalReporter.Responses;

using PropertyChanged;
using LokalReporter;

namespace LokalReporter.App.FormsApp.ViewModels
{
    [ImplementPropertyChanged]
    public class DistrictSettingViewModel : BaseViewModel
    {

        private readonly IArticlesService articlesService;
        private readonly IAsyncSetting<District> setting;
        private District selectedDistrict;
        private int selectedDistrictIndex;

        public DistrictSettingViewModel(IArticlesService articlesService, IUserSettings userSettings)
        {
            this.articlesService = articlesService;
            this.setting = userSettings.DistrictSetting;
        }

        public int SelectedDistrictIndex
        {
            get
            {
                return this.selectedDistrictIndex < 0
                    ? this.SelectedDistrict != null ? this.Districts.IndexOf(this.SelectedDistrict) : -1
                    : this.selectedDistrictIndex;
            }
            set
            {
                this.selectedDistrictIndex = value;
                this.SelectedDistrict = this.Districts.ElementAtOrDefault(value);
            }
        }

        public District SelectedDistrict
        {
            get { return this.selectedDistrict; }
            set
            {
                this.selectedDistrict = value;
                this.setting.SetValueAsync(value);
            }
        }

        public IList<District> Districts { get; set; }

        public IReadOnlyCollection<string> DistrictNames { get; set; }

        public override async void Start()
        {
            base.Start();
            this.Districts = (await this.articlesService.GetDistrictsAsync(this.CloseCancellationToken)).ToList();
            this.DistrictNames = this.Districts.Select(d => d.Name).ToList();
            this.SelectedDistrict = await this.setting.GetValueAsync();
        }

    }
}