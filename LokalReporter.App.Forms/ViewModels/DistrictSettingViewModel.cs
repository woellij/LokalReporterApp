using System.Collections.Generic;
using System.Linq;
using LokalReporter.App.FormsApp.Helpers;
using LokalReporter.Responses;
using PropertyChanged;

namespace LokalReporter.App.FormsApp.ViewModels {

    [ImplementPropertyChanged]
    public class DistrictSettingViewModel : BaseViewModel {
        private readonly IArticlesService articlesService;
        private int selectedDistrictIndex;

        public DistrictSettingViewModel(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
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
            get { return Settings.SelectedDistrict; }
            set { Settings.SelectedDistrict = value; }
        }

        public IList<District> Districts { get; set; }

        public IReadOnlyCollection<string> DistrictNames { get; set; }

        public override async void Start()
        {
            base.Start();
            this.Districts = (await this.articlesService.GetDistrictsAsync(this.CloseCancellationToken)).ToList();
            this.DistrictNames = this.Districts.Select(d => d.Name).ToList();
        }
    }

}